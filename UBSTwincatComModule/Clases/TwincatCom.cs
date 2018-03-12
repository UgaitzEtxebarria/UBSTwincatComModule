using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using TwinCAT.Ads;

namespace UBSTwincatComModule.Clases
{
    public class TwinCatCommunication
    {
        public UBSLib.UBSModule UBSMod { get; set; }
        private TcAdsClient tcAds;
        private TwincatConfig tcConfig;


        private string m_filename = "conf\\IO\\config.xml";
        private string m_filenamefilter = "conf\\IO\\confiFilter.xml";

        private Dictionary<int, TCBlock> m_TCbloques;
        private Dictionary<string, int> m_BlockMap;

        private int varMaxPorBloque = 1000;

        public delegate void TwincatComErrorHandler(object sender, string var_name, string function_name);

        private List<IO_Parameter_Filter> listIO_filter;

        public DateTime lastNotification;

        ///////////////////////////////////////////////////////////////////////////////
        public TwinCatCommunication(UBSLib.UBSModule parent)
        {
            tcAds = new TcAdsClient();
            m_TCbloques = new Dictionary<int, TCBlock>();
            m_BlockMap = new Dictionary<string, int>();
            lastNotification = DateTime.Now;
            UBSMod = parent;
        }
        ///////////////////////////////////////////////////////////////////////////////

        public IO_Parameters IOParameters
        {
            get
            {
                IO_Parameters result = new IO_Parameters();
                foreach (TCBlock bl in m_TCbloques.Values)
                {
                    result.AddIOParameters(bl.IOParameters);
                }
                return result;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        public bool Connect()
        {
            try
            {
                tcAds.Connect(((Modulo.UBSTwincatComModule)UBSMod).IP, ((Modulo.UBSTwincatComModule)UBSMod).Port);

                tcAds.AdsNotificationError += new AdsNotificationErrorEventHandler(adsClient_AdsNotificationError);

                return true;
            }
            catch (Exception e)
            {
                UBSMod.Error("Error al conectar la comunicación con el PLC Beckhoff de " + ((Modulo.UBSTwincatComModule)UBSMod).IP + ". " + e.Message, true, false);
                return false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        private bool ConnectBlocks()
        {

            foreach (TCBlock tcb in m_TCbloques.Values)
            {
                try
                {
                    tcb.Connect();
                    tcb.AñadirRedireccion();
                }
                catch (Exception e)
                {
                    UBSMod.Error("Error al conectar el bloque " + tcb.Id + " de comunicación con el PLC Beckhoff. " + e.Message, true, false);
                    return false;
                }
            }
            return true;
        }

        ///////////////////////////////////////////////////////////////////////////////
        private bool DisconnectBlocks()
        {

            foreach (TCBlock tcb in m_TCbloques.Values)
            {
                try
                {
                    tcb.Disconnect();
                }
                catch (Exception e)
                {
                    UBSMod.Error("Error al desconectar el bloque " + tcb.Id + " de comunicación con el PLC Beckhoff. " + e.Message, true, false);
                    return false;
                }
            }
            return true;
        }

        ///////////////////////////////////////////////////////////////////////////////
        public void InitConfig()
        {
            try
            {
                tcConfig = new TwincatConfig(tcAds);

                //Cargar datos desde fichero o desde el PLC
                if (!LoadIOConf())
                {
                    int cantBloques = (tcConfig.Variables.Keys.Count + varMaxPorBloque - 1) / varMaxPorBloque;

                    for (int i = 0; i < cantBloques; i++)
                    {
                        Dictionary<string, TwincatVariable> aux = SubDict(tcConfig.Variables, i * varMaxPorBloque, varMaxPorBloque);
                        m_TCbloques.Add(i, new TCBlock(i, aux, m_filename, this));
                        m_TCbloques[i].SaveIOConf();
                        m_TCbloques[i].SetWriteDelegate(WriteTwincatValue);
                    }

                    if (!CreateIOConf())
                        UBSMod.Error("Error al crear el archivo de configuración de las variables compartidas. Avisad a Roboconcept.", true, false);
                }
                else
                {

                    AsociarRedirecciones();
                    if (!LeerBloques())
                        UBSMod.Error("Error al cargar los bloques de variables compartidas", true, false);
                    foreach (TCBlock tcb in m_TCbloques.Values)
                    {
                        tcb.SetWriteDelegate(WriteTwincatValue);
                    }
                }

                //Conectar los diferentes bloques al ADS
                ConnectBlocks();


                //Ver que variable se almacena en cada bloque y añadir las redirecciones
                int j = 0;
                foreach (TCBlock tcb in m_TCbloques.Values)
                {
                    //Mapeado de variables
                    foreach (IO_Parameter io in tcb.ListaCompleta)
                        m_BlockMap.Add(io.Name, j);

                    j++;
                }
            }
            catch (Exception e)
            {
                UBSMod.Error("Error al inicializar la configuración de las comunicaciones. " + e.Message, true, false);
            }
        }

        ///////////////////////////////////////////////////////////////////////////

        private bool LeerBloques()
        {
            try
            {
                string path = Path.GetDirectoryName(m_filename) + "\\";
                string filenameWithoutExt = Path.GetFileNameWithoutExtension(m_filename);
                string filenameExt = Path.GetExtension(m_filename);
                string fich = filenameWithoutExt + "*" + filenameExt;

                string[] filenameBloques = Directory.GetFiles(path, fich).Select(path1 => Path.GetFileName(path1)).Where(val => val != Path.GetFileName(m_filename)).ToArray();

                foreach (string filenameBloque in filenameBloques)
                {
                    TCBlock block = new TCBlock(Convert.ToInt32(filenameBloque.Replace(filenameWithoutExt, "").Replace(filenameExt, "")), m_filename, this);
                    m_TCbloques.Add(block.Id, block);
                }

                return true;
            }
            catch (Exception e)
            {
                UBSMod.Error("Error al leer el fichero del bloque de comunicaciones con el PLC Beckhoff. " + e.Message, true, false);
                return false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////

        private bool AsociarRedirecciones()
        {
            try
            {
                XmlDocument xmlRedirecciones = new XmlDocument();

                //La ruta del documento XML permite rutas relativas 
                //respecto del ejecutable!
                xmlRedirecciones.Load(m_filename);

                string[] filenameBloques = Directory.GetFiles("conf\\IO\\", "config*.xml").Select(path => Path.GetFileName(path)).Where(val => val != Path.GetFileName(m_filename)).ToArray();

                //Leer Las listas In y Out
                XmlNodeList listParam = xmlRedirecciones.GetElementsByTagName("ArrayOfIO_Parameter");
                foreach (XmlElement param in listParam)
                {
                    //Leer la lista de Parametros de cada lista
                    XmlNodeList lista = param.GetElementsByTagName("IO_Parameter");
                    foreach (XmlElement nodo in lista)
                    {
                        //Si no tiene redirecciones, pasar al siguiente
                        if (!nodo.LastChild.HasChildNodes)
                            continue;

                        //Si tiene redirecciones hay que buscar en todos los bloques dicho Parametro
                        foreach (string filenameBloque in filenameBloques)
                        {
                            XmlDocument xmlBloque = new XmlDocument();
                            xmlBloque.Load("conf\\IO\\" + filenameBloque);

                            //Leer todos los nombres de variables
                            XmlNodeList listParamBloque = xmlBloque.GetElementsByTagName("Name");
                            foreach (XmlElement nombre in listParamBloque)
                            {
                                //Si el nombre de variable coincide con el Parametro
                                if (nombre.FirstChild.Value == nodo.FirstChild.FirstChild.Value)
                                {
                                    //Recorre todas las redirecciones del Parametro a la variable
                                    foreach (XmlNode redireParam in nodo.LastChild.ChildNodes)
                                    {
                                        //Pero primero hay que verificar que no exista en la variable
                                        bool found = false;
                                        foreach (XmlNode redireVar in nombre.ParentNode.LastChild.ChildNodes)
                                        {
                                            if (redireParam.FirstChild.Value == redireVar.FirstChild.Value)
                                            {
                                                found = true;
                                                break;
                                            }
                                        }
                                        if (!found)
                                        {
                                            XmlNode importNode = nombre.ParentNode.LastChild.OwnerDocument.ImportNode(redireParam, true);
                                            nombre.ParentNode.LastChild.AppendChild(importNode);
                                        }
                                    }
                                }
                            }
                            xmlBloque.Save("conf\\IO\\" + filenameBloque);
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                UBSMod.Error("Archivo de variables compartidas corrupto. Avisad a Roboconcept. " + e.Message, true, false);
                return false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////

        private bool CreateIOConf()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<List<IO_Parameter>>));

                List<List<IO_Parameter>> listIO = new List<List<IO_Parameter>>();
                List<IO_Parameter> listIn = new List<IO_Parameter>();
                List<IO_Parameter> listOut = new List<IO_Parameter>();

                foreach (IO_Parameter IO in IOParameters.In.Values)
                    listIn.Add(IO);

                foreach (IO_Parameter IO in IOParameters.Out.Values)
                    listOut.Add(IO);

                listIO.Add(listIn);
                listIO.Add(listOut);

                FileStream filestream = new FileStream(this.m_filename, FileMode.Create);

                try
                {
                    serializer.Serialize(filestream, listIO);
                }
                catch (Exception e1)
                {
                    filestream.Close();
                    UBSMod.Error("Error al serializar el bloque de comunicación con el PLC Beckhoff. " + e1.Message, true, false);
                }

                filestream.Close();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////

        private List<IO_Parameter> getAllIOParametes()
        {
            List<IO_Parameter> result = new List<IO_Parameter>();
            foreach (TCBlock tcb in m_TCbloques.Values)
                result = result.Concat(tcb.ListaCompleta).ToList();
            return result;
        }

        ///////////////////////////////////////////////////////////////////////////

        private Dictionary<string, TwincatVariable> SubDict(Dictionary<string, TwincatVariable> dict, int indIni, int length)
        {
            Dictionary<string, TwincatVariable> result = new Dictionary<string, TwincatVariable>();

            List<string> keys = dict.Keys.Skip(indIni).Take(length).ToList();
            List<TwincatVariable> values = dict.Values.Skip(indIni).Take(length).ToList();

            result = keys.Zip(values, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////
        private bool LoadIOConf()
        {
            //carga el filtro
            IO_Parameter_Filter IO_flters = new IO_Parameter_Filter();
            XmlSerializer serializerFilter = new XmlSerializer(typeof(List<IO_Parameter_Filter>));
            FileStream filestreamFilter;

            try
            {
                filestreamFilter = new FileStream(this.m_filenamefilter, FileMode.Open);
                listIO_filter = (List<IO_Parameter_Filter>)serializerFilter.Deserialize(filestreamFilter);
                filestreamFilter.Close();
            }
            catch (Exception e1)
            {
                //no existe el fichero genera unos con todas las variables
                filestreamFilter = new FileStream(this.m_filenamefilter, FileMode.Create);
                listIO_filter = new List<IO_Parameter_Filter>();
                IO_Parameter_Filter IO_filter = new IO_Parameter_Filter();
                IO_filter.Name.Add("UBS");
                //IO_filter.Redirections.Add("modulo");
                listIO_filter.Add(IO_filter);
                serializerFilter.Serialize(filestreamFilter, listIO_filter);
                filestreamFilter.Close();
                // listIO_filter.Clear();
            }

            if (Directory.GetFiles("conf\\IO\\", "config.xml").Select(path => Path.GetFileName(path)).ToArray().Length > 0)
                return true;


            return false;
        }

        public bool CumpleFiltrado(ref IO_Parameter ioParam)
        {
            bool cumple = false;
            foreach (IO_Parameter_Filter IOFilter in listIO_filter)
            {
                bool Existe_condicion = false;
                string expresion = ioParam.Name;
                for (int i = 0; i < IOFilter.Name.Count; i++)
                {
                    MatchCollection resultado;

                    string expresion_regular = IOFilter.Name[i];
                    resultado = Regex.Matches(expresion, expresion_regular);
                    if (resultado.Count > 0)
                        Existe_condicion = true;
                    else
                    {
                        Existe_condicion = false;
                        break;
                    }
                }

                if (Existe_condicion)
                {
                    int count = IOFilter.Redirections.Count;
                    for (int i = 0; i < count; i++)
                        ioParam.Redirections.Add(IOFilter.Redirections[i]);
                    cumple = true;
                    break;
                }
            }
            return cumple;
        }


        ///////////////////////////////////////////////////////////////////////////
        //public void Save()
        //{
        //    this.SaveIOConf(this.IOparameters);
        //}

        ///////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////

        private void WriteTwincatValue(string name, object value)
        {
            try
            {
                if (this.Config.Variables.ContainsKey(name))
                    this.tcAds.WriteSymbol(Config.Variables[name].Symbol, value);
            }
            catch (Exception e)
            {
                if (e.Message == "Cannot convert object to symbol type.")
                    UBSMod.Error("La variable compartida " + name + " no tiene el mismo tipo que el enviado. (Guardad el texto del error y avisad al administrador del sistema).", true, false);
                else
                    UBSMod.Error("Error al escribir la variable en el PLC. " + e.Message, true, false);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////

        public void ForceReadTwincatValues()
        {
            try
            {
                Dictionary<string, object> dict = ReadValues();

                foreach (KeyValuePair<string, object> kvpair in dict)
                    m_TCbloques[m_BlockMap[kvpair.Key]].IOParameters.Set(kvpair.Key, kvpair.Value);
            }
            catch (Exception e)
            {
                UBSMod.Error("Error al forzar la lectura de variables compartidas. Avisad al administrador del sistema. " + e.Message, true, false);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        public TwincatConfig Config { get { return tcConfig; } }

        ///////////////////////////////////////////////////////////////////////////////
        public bool Disconnect()
        {
            try
            {
                DisconnectBlocks();
                tcAds.Dispose();
                return true;
            }
            catch (Exception e)
            {
                UBSMod.Error("Error al desconectar los bloques de comunicación. " + e.Message, true, false);
                return false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        public bool Reconnect()
        {
            try
            {
                Disconnect();
                Connect();
                ConnectBlocks();
                return true;
            }
            catch (Exception e)
            {
                UBSMod.Error("Error al desconectar los bloques de comunicación. " + e.Message, true, false);
                return false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        private void adsClient_AdsNotificationError(object sender, AdsNotificationErrorEventArgs e)
        {

        }

        ///////////////////////////////////////////////////////////////////////////////
        private void adsClient_AdsStateChanged(object sender, AdsStateChangedEventArgs e)
        {

        }

        private Dictionary<string, object> ReadValues()
        {
            try
            {
                DateTime t1 = DateTime.Now;
                Dictionary<string, object> values = new Dictionary<string, object>();
                foreach (TwincatVariable variable in this.Config.Variables.Values)
                    values.Add(variable.Symbol.Name, this.tcAds.ReadSymbol(variable.Symbol));

                DateTime t2 = DateTime.Now;
                values.Add("tElapsed: ", (t2 - t1).TotalMilliseconds.ToString());
                return values;
            }
            catch (Exception e)
            {
                UBSMod.Error("Error al leer las variables del PLC Beckhoff. " + e.Message, true, false);
                return null;
            }
        }
    }
}



