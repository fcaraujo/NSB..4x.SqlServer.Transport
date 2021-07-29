using System;

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
                Installer.Run();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR - Process failed due to exception: {ex.Message}.");
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
