using NServiceBus;
using NServiceBus.Logging;

namespace NSB.SqlServer.Consumer
{
    public class Consumer 
        : IWantToRunWhenBusStartsAndStops
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Consumer));
        
        private const string MARKER = "\n= = = = =\n";

        /// <summary>
        /// IWantToRunWhenBusStartsAndStops method responsible to be ran after NSB setup
        /// </summary>
        public void Start()
        {
            Log.Info($"{MARKER}Consumer is STARTED and ready for messages!{MARKER}");
        }

        /// <summary>
        /// IWantToRunWhenBusStartsAndStops method responsible to be ran after the NSB shutdown
        /// </summary>
        public void Stop()
        {
            Log.Info($"{MARKER}Consumer is now STOPPED!{MARKER}");
        }
    }
}
