using System;
using System.Linq;
using System.Threading;
using UBSTwincatComModule.Clases;
using UBSTwincatComModule.Vista;

namespace UBSTwincatComModule.Modulo
{
    public abstract class UBSTwincatComModule : UBSLib.UBSVisualModule
    {
        //////////////////////////////////////////////////////////////////////////////////
        protected TwinCatCommunication m_twincat_com;

        protected Thread thrRefreshTC;
        protected bool needUpdate;

        private DateTime lastConection;
        protected int connectionTimeout = 5;

        protected string ip;
        protected int port;
        protected string watchdogVar;

        private bool isConnected;

        //////////////////////////////////////////////////////////////////////////////////
        public UBSTwincatComModule(string _id)
            : base(_id)
        { }

        //////////////////////////////////////////////////////////////////////////////////
        public override bool Init()
        {
            base.Init();
            WindowForm = new TwincatComForm(this);

            isConnected = false;

            m_twincat_com = new TwinCatCommunication(this);
            m_twincat_com.OnReadError += OnTwincatReadError;
            m_twincat_com.OnReadError += OnTwincatWriteError;

            ((TwincatComForm)this.WindowForm).Leer += new EventHandler(Leer);
            ((TwincatComForm)this.WindowForm).SelectedInChanged += new EventHandler(DisplayInput);
            ((TwincatComForm)this.WindowForm).SelectedOutChanged += new EventHandler(DisplayOutput);

            SetIpPortWatchdogVar();

            if (!m_twincat_com.Connect())
                Error("No se puede conectar al PLC.", true, false);

            try
            {
                m_twincat_com.InitConfig();

                this.SetGlobalParameter("IO", m_twincat_com.IOParameters);
            }
            catch (Exception e)
            {
                Error("Error al iniciar la configuración Twincat. " + e.Message);
            }

            SuscribeRedirections();

            ((TwincatComForm)this.WindowForm).SetSourceIn(this.m_twincat_com.IOParameters.ToList(true));
            ((TwincatComForm)this.WindowForm).SetSourceOut(this.m_twincat_com.IOParameters.ToList(false));

            thrRefreshTC = new Thread(TCUpdater);
            thrRefreshTC.Name = "TCUpdater";
            thrRefreshTC.IsBackground = true;
            thrRefreshTC.Start();
            needUpdate = false;

            ResetMaquina();

            SetGlobalParameter("Communication", "twincat");

            lastConection = DateTime.Now;

            WriteConsole("Modulo cargado correctamente.");
            return true;
        }

        //////////////////////////////////////////////////////////////////////////////////

        protected virtual void SetIpPortWatchdogVar()
        {
            throw new NotImplementedException();
        }

        //////////////////////////////////////////////////////////////////////////////////
        public override bool Destroy()
        {
            m_twincat_com.Disconnect();
            thrRefreshTC.Join();
            return true;
        }

        //////////////////////////////////////////////////////////////////////////////////
        public bool Reconnect()
        {
            m_twincat_com.Reconnect();
            return true;
        }

        //////////////////////////////////////////////////////////////////////////////////

        protected virtual void ResetMaquina()
        {
            throw new NotImplementedException();
        }

        //////////////////////////////////////////////////////////////////////////////////

        protected virtual void Disconnected()
        {
            throw new NotImplementedException();
        }

        //////////////////////////////////////////////////////////////////////////////////

        protected virtual void Reconnected()
        {
            throw new NotImplementedException();
        }

        //////////////////////////////////////////////////////////////////////////////////
        public override void HandleMessages(UBSLib.UBSMessage message)
        {
            string strMessage = message.ToString();

            //Parametros del mensaje
            string[] strParams = strMessage.Split('#');
            //

            if (strParams[0] == watchdogVar)
                lastConection = DateTime.Now;
        }

        //////////////////////////////////////////////////////////////////////////////////

        private void TCUpdater()
        {
            lastConection = DateTime.Now;
            while (Status != UBSLib.UBSModuleStatus.Closing && Status != UBSLib.UBSModuleStatus.Closed)
            {
                Thread.Sleep(100);

                if ((DateTime.Now - lastConection).TotalSeconds > connectionTimeout)
                {
                    Disconnected();
                    isConnected = false;
                    //((TwincatForm)WindowForm).Reconnect();
                }
                else
                {
                    if (!isConnected)
                        Reconnected();
                    isConnected = true;
                }

                //Si el modulo esta activo actualizar, sino no gastar recursos del PC
                if (needUpdate)
                {
                    needUpdate = false;
                    if (WindowForm.IsDisposed)
                        return;
                    ((TwincatComForm)this.WindowForm).IOUpdate();
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////
        public void OnTwincatReadError(object sender, string var_name, string function_name)
        {
            Error("Error leyendo variable Twincat. Nombre Variable:\"" + var_name + "\" Función:\"" + function_name + "\"", true, false);
        }

        ///////////////////////////////////////////////////////////////////////
        public void OnTwincatWriteError(object sender, string var_name, string function_name)
        {
            Error("Error escribiendo variable Twincat. Nombre Variable:\"" + var_name + "\" Función:\"" + function_name + "\"", true, false);
        }

        ///////////////////////////////////////////////////////////////////////
        private void Leer(object sender, EventArgs e)
        {
            this.m_twincat_com.ForceReadTwincatValues();
        }

        ///////////////////////////////////////////////////////////////////////
        private void DisplayInput(object sender, EventArgs e)
        {
            try
            {
                string comment = "No se ha podido leer la variable.";
                string valueStr = "-";
                if (m_twincat_com.Config.Variables.ContainsKey(sender.ToString()))
                {
                    comment = m_twincat_com.Config.Variables[sender.ToString()].Symbol.Comment;
                    valueStr = m_twincat_com.IOParameters.In[sender.ToString()].Value.ToString();
                }

                ((TwincatComForm)this.WindowForm).DisplayIO(true, comment, valueStr);

            }
            catch (Exception er)
            {
                ((TwincatComForm)this.WindowForm).ErrorDisplaying(true);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        private void DisplayOutput(object sender, EventArgs e)
        {
            try
            {
                string comment = "No se ha podido leer la variable.";
                string valueStr = "-";
                if (m_twincat_com.Config.Variables.ContainsKey(sender.ToString()))
                {
                    comment = m_twincat_com.Config.Variables[sender.ToString()].Symbol.Comment;
                    valueStr = m_twincat_com.IOParameters.Out[sender.ToString()].Value.ToString();
                }
                ((TwincatComForm)this.WindowForm).DisplayIO(false, comment, valueStr);
            }
            catch (Exception er)
            {
                ((TwincatComForm)this.WindowForm).ErrorDisplaying(false);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        public void IODisplayUpdate()
        {
            needUpdate = true;
        }

        ///////////////////////////////////////////////////////////////////////
        private void VariableRedirection(object sender, EventArgs e)
        {
            IO_Parameter IO = (IO_Parameter)sender;
            foreach (string dest in IO.Redirections)
                SendMessage(dest, IO.Name + "#" + IO.Value.ToString());
        }

        ///////////////////////////////////////////////////////////////////////
        private void SuscribeRedirections()
        {
            foreach (IO_Parameter IO in this.m_twincat_com.IOParameters.In.Values)
            {
                if (IO.Redirections.Count > 0)
                    IO.OnChange += VariableRedirection;
            }

            foreach (IO_Parameter IO in this.m_twincat_com.IOParameters.Out.Values)
            {
                if (IO.Redirections.Count > 0)
                    IO.OnChange += VariableRedirection;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////

        public string IP
        {
            get { return ip; }
        }

        public int Port
        {
            get { return port; }
        }
    }
}
