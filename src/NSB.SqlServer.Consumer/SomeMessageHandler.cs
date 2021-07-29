using Newtonsoft.Json;
using NSB.SqlServer.Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace NSB.SqlServer.Consumer
{
    /// <summary>
    /// Message handler, here we can do our business
    /// </summary>
    public class SomeMessageHandler
        : IHandleMessages<SomeMessage>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SomeMessageHandler));

        private const string MARKER = "\n= = = = =\n";

        /// <summary>
        /// Handle messages, in this case we're just printing it :)
        /// </summary>
        /// <param name="message"></param>
        public void Handle(SomeMessage message)
        {
            var serializedMessage = JsonConvert.SerializeObject(message, Formatting.Indented);

            Log.Info($"{MARKER}Message RECEIVED:\n{serializedMessage}{MARKER}");
        }
    }
}
