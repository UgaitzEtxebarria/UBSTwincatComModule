using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TwinCAT.Ads;

namespace UBSTwincatComModule.Clases
{
    public class TwincatConfig
    {
        public Dictionary<string, TwincatVariable> Variables { get; set; }

        private TcAdsClient m_tcAds;
        private TcAdsSymbolInfoLoader m_symbolLoader;

        private Dictionary<string, Type> m_tipos = new Dictionary<string, Type>
        {
            {"BOOL", typeof(Boolean)},
            {"BYTE", typeof(Byte)},
            {"WORD", typeof(ushort)},
            {"DWORD", typeof(uint)},
            {"SINT", typeof(sbyte)},
            {"INT16", typeof(short)},
            {"DINT", typeof(int)},
            {"LINT", typeof(long)},
            {"USINT", typeof(Byte)},
            {"UINT", typeof(ushort)},
            {"UDINT", typeof(uint)},
            {"ULINT", typeof(ulong)},
            {"REAL", typeof(float)},
            {"LREAL", typeof(Double)},
            {"STRING(80)", typeof(String)},
        };

        ///////////////////////////////////////////////////////////////////////////////
        public TwincatConfig(TcAdsClient _tcAds)
        {
            string type = "";
            try
            {
                this.m_tcAds = _tcAds;
                this.Variables = new Dictionary<string, TwincatVariable>();

                this.m_symbolLoader = this.m_tcAds.CreateSymbolInfoLoader();



                foreach (TcAdsSymbolInfo symbol in this.m_symbolLoader)
                {
                    if (symbol.Name.Contains("UBS_"))
                    {
                        type = symbol.Type.ToString();

                        if (this.m_tipos.ContainsKey(type))
                            this.Variables.Add(symbol.Name, new TwincatVariable(symbol, this.m_tipos[type]));
                    }

                }

            }
            catch (Exception e)
            {
                MessageBox.Show(new Form() { TopMost = true }, type + e.ToString(), "Error", MessageBoxButtons.OK);
            }
        }
    }
}
