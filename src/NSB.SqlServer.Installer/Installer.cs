using System;
using System.Data.SqlClient;
using System.Linq;

namespace NSB.SqlServer.Installer
{
    public static class Installer
    {
        public static void Run(string connectionString, string schema, string[] endpointNames)
        {
            // Validations
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or empty.", nameof(connectionString));
            }

            if (string.IsNullOrEmpty(schema))
            {
                throw new ArgumentException($"'{nameof(schema)}' cannot be null or empty.", nameof(schema));
            }

            if (endpointNames is null)
            {
                throw new ArgumentNullException($"{nameof(endpointNames)} cannot be null or empty.", nameof(endpointNames));
            }
            
            var validEndpoints = endpointNames.Distinct().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            if (!validEndpoints.Any())
            {
                throw new ArgumentNullException($"{nameof(endpointNames)} should have at least one value.", nameof(endpointNames));
            }

            // Let's connect and create the tables  
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                Console.WriteLine($"Creating tables using schema '{schema}'.");

                CreateSharedQueues(connection, schema);

                Console.WriteLine($"Table for shared queues audit/error are created.");

                foreach (var endpointName in validEndpoints)
                {
                    CreateQueuesForEndpoint(connection, schema, endpointName);

                    Console.WriteLine($"Tables for endpoint '{endpointName}' are created.");
                }
            }
        }

        private static void CreateQueuesForEndpoint(SqlConnection connection, string schema, string endpointName)
        {
            // main queue
            QueueCreationUtils.CreateQueue(connection, schema, endpointName);

            // callback queue
            QueueCreationUtils.CreateQueue(connection, schema, $"{endpointName}.{Environment.MachineName}");

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

        private static void CreateSharedQueues(SqlConnection sqlConnection, string schema)
        {
            QueueCreationUtils.CreateQueue(sqlConnection, schema, "audit");

            QueueCreationUtils.CreateQueue(sqlConnection, schema, "error");
        }
    }
}
