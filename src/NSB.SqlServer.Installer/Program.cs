using System;
using System.Configuration;

namespace NSB.SqlServer.Installer
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "NSB.SqlServer.Installer";

            Console.WriteLine("Running installer, please wait.");
            Console.WriteLine();

            try
            {
                var connectionStringSettings = ConfigurationManager.ConnectionStrings["NServiceBus/Transport"];
                string endpointSettings = ConfigurationManager.AppSettings.Get("EndpointNames");

                var connectionString = connectionStringSettings?.ConnectionString;
                var endpointNames = endpointSettings?.Split(',');
                var schema = ConfigurationManager.AppSettings.Get("SqlServerSchema");

                Installer.Run(connectionString, schema, endpointNames);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR - Process failed due to exception: {ex.Message}");
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("Installer finished, press any key to close.");

                Console.ReadKey();
            }            
        }
    }
}
