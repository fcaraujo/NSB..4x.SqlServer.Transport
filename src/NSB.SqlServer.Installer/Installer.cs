using System;
using System.Configuration;
using System.Data.SqlClient;

namespace NSB.SqlServer.Installer
{
    public static class Installer
    {
        public static void Run()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["NServiceBus/Transport"].ConnectionString ?? "Not found";
            var endpointName = ConfigurationManager.AppSettings.Get("EndpointName") ?? "Not found";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                CreateQueuesForEndpoint(connection, "dbo", endpointName);

                Console.WriteLine($"Table for endpoint '{endpointName}' is created.");
            }
        }

        public static void CreateQueuesForEndpoint(SqlConnection connection, string schema, string endpointName)
        {
            // main queue
            QueueCreationUtils.CreateQueue(connection, schema, endpointName);

            // callback queue - TODO: double check if it's required
            //QueueCreationUtils.CreateQueue(connection, schema, $"{endpointName}.{Environment.MachineName}");

            // delayed messages queue
            // Only required in Version 3.1 and above when native delayed delivery is enabled
            //QueueCreationUtils.CreateDelayedQueue(connection, schema, $"{endpointName}.Delayed");

            // timeout queue
            // only required in Versions 3.0 and below or when native delayed delivery is disabled or timeout manager compatibility mode is enabled
            QueueCreationUtils.CreateQueue(connection, schema, $"{endpointName}.Timeouts");

            // timeout dispatcher queue
            // only required in Versions 3.0 and below or when native delayed delivery is disabled or timeout manager compatibility mode is enabled
            QueueCreationUtils.CreateQueue(connection, schema, $"{endpointName}.TimeoutsDispatcher");

            // retries queue
            // TODO: Only required in Versions 2 and below
            QueueCreationUtils.CreateQueue(connection, schema, $"{endpointName}.Retries");
        }


    }
}
