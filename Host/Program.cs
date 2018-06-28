using System;
using System.ServiceModel;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("############################################\n");
            Console.WriteLine("          Microsoft Platform Host \n");
            Console.WriteLine("############################################\n");

            Console.WriteLine("Initialization of the Endpoints.....");
            ServiceHost host = new ServiceHost(typeof(MicrosoftService.Service));

            Console.WriteLine("Services are listening.....\n");

            Console.WriteLine("List of Available Endpoints : ");

            foreach (var endpoint in host.Description.Endpoints)
            {
                Console.WriteLine("\t- Binding : {0}  |  Address : {1}", endpoint.Binding, endpoint.Address);
            }

            host.Open();
            Console.WriteLine("\nPress enter to quit.....");
            Console.ReadLine();
            host.Close();
        }
    }
}
