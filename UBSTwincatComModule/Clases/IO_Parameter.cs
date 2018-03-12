using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace UBSTwincatComModule.Clases
{
    public class IO_Parameter
    {
        public string Name { get; set; }

        public string TypeStr { get; set; }

        public object Value { get; set; }

        public List<string> Redirections { get; set; } = new List<string>();

        public delegate void TwincatWriteHandler(string variableName, object value);

        public event EventHandler OnChange;
        public event TwincatWriteHandler OnWrite;

        public IO_Parameter() { }
        public IO_Parameter(string name, Type type, int maxLength = -1)
        {
            this.Name = name;
            this.TypeStr = type.ToString();

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    this.Value = false;
                    break;
                case TypeCode.String:
                    this.Value = "";
                    break;
                default:
                    this.Value = 0;
                    break;
            }
        }

        public IO_Parameter(string name, Type type, TwincatWriteHandler WriteDelegate, int maxLength = -1) : this(name, type, maxLength)
        {
            this.OnWrite += WriteDelegate;
        }

        public void Set(object value)
        {
            bool onChangeBool = false;
            if (!this.Value.Equals(value))
                onChangeBool = true;
            this.Value = value;
            if ((onChangeBool) && (OnChange != null))
                OnChange(this, null);
        }

        public void EnviarMensajes()
        {
            OnChange?.Invoke(this, null);
        }

        public void Write(object value)
        {
            this.Value = value;
            OnWrite?.Invoke(this.Name, value);
        }

        [XmlIgnoreAttribute]
        protected Type Type
        {
            get { return Type.GetType(this.TypeStr); }
            set { this.TypeStr = value.ToString(); }
        }

    }

    public class IO_Parameter_Filter
    {
        private List<string> m_name = new List<string>();
        private List<string> m_redirections = new List<string>();

        public IO_Parameter_Filter() { }

        public List<string> Name
        {
            get { return this.m_name; }
            set { this.m_name = value; }
        }

        public List<string> Redirections
        {
            get { return this.m_redirections; }
            set { this.m_redirections = value; }
        }
    }
}
