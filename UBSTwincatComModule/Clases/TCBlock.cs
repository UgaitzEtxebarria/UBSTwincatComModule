using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TwinCAT.Ads;

namespace UBSTwincatComModule.Clases
{
    public class TCBlock
    {
        private TwinCatCommunication parent;
        private string m_filename = "";
        private TcAdsClient tcAds;
        private IO_Parameters IOparameters;

        private Dictionary<int, string> notifications;
        private AdsStream stream;
        private BinaryReader reader;

        private int stringMaxSize;

        private int m_id;

        public TCBlock(int id, Dictionary<string, TwincatVariable> dict, string filename, TwinCatCommunication comm)
        {
            m_id = id;
            m_filename = filename.Insert(filename.LastIndexOf('.'), id.ToString());
            parent = comm;
            tcAds = new TcAdsClient();
            notifications = new Dictionary<int, string>();
            IOparameters = new IO_Parameters(dict);

        }

        public TCBlock(int id, string fullFilename, TwinCatCommunication comm)
        {
            string path = Path.GetDirectoryName(fullFilename) + "\\";
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(fullFilename);
            string filenameExt = Path.GetExtension(fullFilename);
            m_id = id;
            m_filename = path + filenameWithoutExt + id + filenameExt;
            parent = comm;
            notifications = new Dictionary<int, string>();
            IOparameters = new IO_Parameters();
            tcAds = new TcAdsClient();
            stringMaxSize = 80;

            LoadIOConf();
        }

        public bool Init()
        {

            return true;
        }

        public bool Connect()
        {
            try
            {
                tcAds.Connect(((Modulo.UBSTwincatComModule)parent.UBSMod).IP, ((Modulo.UBSTwincatComModule)parent.UBSMod).Port);
                tcAds.AdsNotificationError += new AdsNotificationErrorEventHandler(adsClient_AdsNotificationError);

                return true;
            }
            catch (Exception e)
            {
                parent.UBSMod.Error("Error al conectar el bloque " + m_id + " de comunicaciones. " + e.Message, true, false);
                return false;
            }
        }

        public bool Disconnect()
        {
            try
            {
                tcAds.Dispose();
                return true;
            }
            catch (Exception e)
            {
                parent.UBSMod.Error("Error al desconectar el bloque " + m_id + " de comunicaciones. " + e.Message, true, false);
                return false;
            }
        }

        public bool SaveIOConf()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<List<IO_Parameter>>));

                List<List<IO_Parameter>> listIO = new List<List<IO_Parameter>>();
                List<IO_Parameter> listIn = new List<IO_Parameter>();
                List<IO_Parameter> listOut = new List<IO_Parameter>();

                foreach (IO_Parameter IO in IOparameters.In.Values)
                    listIn.Add(IO);

                foreach (IO_Parameter IO in IOparameters.Out.Values)
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
                    throw (e1);
                }

                filestream.Close();

                return true;
            }
            catch (Exception e)
            {
                parent.UBSMod.Error("Error al guardar el bloque de comunicacion. Avisad a Roboconcept. " + e.Message, true);
                return false;
            }
        }
        ///////////////////////////////////////////////////////////////////////////

        public bool LoadIOConf()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<List<IO_Parameter>>));
                FileStream filestream = new FileStream(this.m_filename, FileMode.Open);
                List<List<IO_Parameter>> listIO;
                try
                {
                    listIO = (List<List<IO_Parameter>>)serializer.Deserialize(filestream);
                }
                catch (Exception e1)
                {
                    filestream.Close();
                    throw (e1);
                }

                filestream.Close();

                for (int i = 0; i < listIO[0].Count; i++)
                {
                    IO_Parameter IO = listIO[0][i];
                    if (parent.CumpleFiltrado(ref IO))
                        IOparameters.In.Add(IO.Name, IO);
                }


                for (int i = 0; i < listIO[1].Count; i++)
                {
                    IO_Parameter IO = listIO[1][i];
                    if (parent.CumpleFiltrado(ref IO))
                        IOparameters.Out.Add(IO.Name, IO);
                }


                return true;
            }
            catch (Exception e)
            {
                parent.UBSMod.Error("Error al cargar el bloque de comunicacion número " + m_id + ". Avisad a Roboconcept. " + e.Message, true);
                return false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        private void adsClient_AdsNotificationError(object sender, AdsNotificationErrorEventArgs e)
        {
            if (parent.UBSMod.Status != UBSLib.UBSModuleStatus.Closing && parent.UBSMod.Status != UBSLib.UBSModuleStatus.Closed)
                parent.UBSMod.Error("Error en las notificaciones del ADS. " + e.Exception.Message, true, false);
        }

        ///////////////////////////////////////////////////////////////////////////
        public void SetWriteDelegate(IO_Parameter.TwincatWriteHandler WriteTwincatValue)
        {
            IOparameters.SetWriteDelegate(WriteTwincatValue);
        }

        ///////////////////////////////////////////////////////////////////////////

        public void AñadirRedireccion()
        {
            int tamaño = 0;
            notifications.Clear();

            foreach (IO_Parameter IO in IOparameters.Out.Values)
                tamaño += getTypeSize(Type.GetType(IO.TypeStr));

            stream = new AdsStream(tamaño);
            reader = new BinaryReader(stream, Encoding.ASCII);

            string asd = "";
            tamaño = 0;
            try
            {
                tcAds.AdsNotification -= new AdsNotificationEventHandler(OnNotification);

                foreach (IO_Parameter IO in IOparameters.Out.Values)
                {
                    asd = IO.Name;
                    notifications.Add(tcAds.AddDeviceNotification(IO.Name, stream, tamaño, getTypeSize(Type.GetType(IO.TypeStr)), AdsTransMode.OnChange, 1, 1, IO.Value), IO.Name);
                    tamaño += getTypeSize(Type.GetType(IO.TypeStr));
                }
            }
            catch (Exception e)
            {
                parent.UBSMod.Error("Error al añadir la redirección. " + e.Message, true, false);
            }

            tcAds.AdsNotification += new AdsNotificationEventHandler(OnNotification);
        }

        ///////////////////////////////////////////////////////////////////////////

        private int getTypeSize(Type type)
        {
            switch (type.FullName)
            {
                case "System.Boolean":
                case "System.Byte":
                    return 1;
                case "System.UInt32":
                    return 4;
                case "System.Int64":
                    return 8;
                case "System.String":
                    return stringMaxSize;

                default:
                    parent.UBSMod.Error("Tipo de dato " + type.FullName + " no administrado, imposible extraer tamaño.", true, false);
                    return -1;
            }
        }

        /////////////////////////////////

        private void OnNotification(object sender, AdsNotificationEventArgs e)
        {
            string var = notifications[e.NotificationHandle];

            parent.UBSMod.WriteExecutionTime(var + " recibido. *" + (DateTime.Now - parent.lastNotification).TotalMilliseconds + "ms*");
            parent.lastNotification = DateTime.Now;

            Type tipo;
            if (IOparameters.In.ContainsKey(var))
                tipo = Type.GetType(IOparameters.In[var].TypeStr);
            else if (IOparameters.Out.ContainsKey(var))
                tipo = Type.GetType(IOparameters.Out[var].TypeStr);
            else
            {
                parent.UBSMod.Error("No se puede notificar la variable " + var + " por que no existe en el sistema.", true, false);
                tipo = typeof(Boolean);
            }

            DateTime time = DateTime.Now;
            parent.UBSMod.WriteExecutionTime("Variable " + var + " to process.");
            object obj = leerValor(tipo);
            IOparameters.Set(var, obj);
            parent.UBSMod.WriteExecutionTime("Variable " + var + " processed [" + (DateTime.Now - time).TotalMilliseconds + "].");

            ((Modulo.UBSTwincatComModule)parent.UBSMod).IODisplayUpdate();
        }

        ///////////////////////////////////////////////////////////////////////////


        private object leerValor(Type tipo)
        {
            try
            {
                object result = null;
                if (reader.BaseStream.CanRead)
                {
                    if (tipo == typeof(UInt32))
                        result = reader.ReadUInt32();
                    else if (tipo == typeof(Boolean))
                        result = reader.ReadBoolean();
                    else if (tipo == typeof(Int64))
                        result = reader.ReadInt64();
                    else if (tipo == typeof(Byte))
                        result = reader.ReadByte();
                    else if (tipo == typeof(UInt16))
                        result = reader.ReadUInt16();
                    else if (tipo == typeof(float))
                        result = reader.ReadSingle();
                    else if (tipo == typeof(String))
                    {
                        char[] caharr = reader.ReadChars(stringMaxSize);
                        string Texto = "";

                        for (int i = 0; i < stringMaxSize; i++)
                        {
                            if (caharr[i].ToString() == "/0")
                                break;
                            Texto = Texto + caharr[i].ToString();
                        }
                        result = Texto;
                    }
                    else
                        parent.UBSMod.Error("Tipo inesperado: " + tipo.ToString(), true, false);

                }
                return result;
            }
            catch (Exception e)
            {
                parent.UBSMod.Error("Error al leer el valor de tipo " + tipo + ". " + e.Message, true, false);
                return null;
            }
        }

        ///////////////////////////////////////////////////////////////////////////

        public IO_Parameters IOParameters
        {
            get { return IOparameters; }
        }

        public bool setRedirections(string paramName, List<string> moduleNames)
        {
            return IOparameters.setRedirections(paramName, moduleNames);
        }

        public List<IO_Parameter> ListaCompleta
        {
            get { return IOparameters.ToList(true).Concat(IOparameters.ToList(false)).ToList(); }
        }

        public int Id
        {
            get { return m_id; }
        }
    }
}
