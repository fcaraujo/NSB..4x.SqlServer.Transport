using System;
using Newtonsoft.Json;
using NSB.SqlServer.Messages;
using NServiceBus;
using NServiceBus.Logging;

namespace NSB.SqlServer.Producer
{
    public class SomeMessageProducer 
        : IWantToRunWhenBusStartsAndStops
    {
        public IBus Bus { get; set; }

        private static readonly ILog Log = LogManager.GetLogger(typeof(SomeMessageProducer));

        private const string MARKER = "\n= = = = =\n";

        /// <summary>
        /// IWantToRunWhenBusStartsAndStops method responsible to be ran after NSB setup - it gets a Bus object and sends the message(s)
        /// </summary>
        public void Start()
        {
            Log.Info($"{MARKER}Producer STARTED! \nPress ENTER to create and send some message...{MARKER}");

            while (Console.ReadLine() != null)
            {
                var message = CreateMessage();

                Bus.Send(message);

                Log.Info($"{MARKER}Message SENT!{MARKER}");
            }
        }

        /// <summary>
        /// IWantToRunWhenBusStartsAndStops method responsible to be ran after the NSB shutdown
        /// </summary>
        public void Stop()
        {
            Log.Info($"{MARKER}Producer STOPPED!{MARKER}");
        }

        /// <summary>
        /// Creates a message with some random properties :)
        /// </summary>
        /// <returns>SomeMessage</returns>
        private static SomeMessage CreateMessage()
        {
            var random = new Random();
            var customerId = random.Next(1, 20);
            var createdAt = DateTime.Now;
            var message = new SomeMessage(customerId, createdAt);

            // Mimicking some random business rule
            if (customerId % 2 == 0)
            {
                message.SetIsTruth(false);
            }

            // We're not using structured logging :( so let's just serialize it to show the object
            var serializedMessage = JsonConvert.SerializeObject(message, Formatting.Indented);
            Log.Info($"{MARKER}Message created:\n{serializedMessage}{MARKER}");

            return message;
        }
    }
}
