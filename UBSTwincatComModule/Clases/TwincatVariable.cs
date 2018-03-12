using System;
using TwinCAT.Ads;

namespace UBSTwincatComModule.Clases
{
    public class TwincatVariable
    {
        public TcAdsSymbolInfo Symbol { get; set; }

        public int Handle { get; set; } = -1;

        public Type Type { get; set; }

        public TwincatVariable(TcAdsSymbolInfo symbol, Type type)
        {
            this.Symbol = symbol;
            this.Type = type;
        }

    }
}
