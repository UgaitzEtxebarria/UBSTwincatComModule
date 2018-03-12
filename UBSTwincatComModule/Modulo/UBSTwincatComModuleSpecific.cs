

namespace UBSTwincatComModule.Modulo
{
    public class UBSTwincatComModuleSpecific : UBSTwincatComModule
    {
        public UBSTwincatComModuleSpecific(string _id) : base(_id)
        {

        }

        //////////////////////////////////////////////////////////////////////////////////

        protected override void SetIpPortWatchdogVar()
        {
            ip = "X.X.X.X";
            port = 0;
            watchdogVar = "XX";
        }

        //////////////////////////////////////////////////////////////////////////////////

        protected override void ResetMaquina()
        {
            
        }

        //////////////////////////////////////////////////////////////////////////////////

        protected override void Disconnected()
        {

        }

        //////////////////////////////////////////////////////////////////////////////////

        protected override void Reconnected()
        {

        }

        //////////////////////////////////////////////////////////////////////////////////
    }
}
