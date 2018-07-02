using System;
using System.ServiceModel;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("#############################################\n");
            Console.WriteLine("        Microsoft Platform Device Host \n");
            Console.WriteLine("#############################################\n");

            Console.WriteLine("Initialization of the Endpoints.....");
            ServiceHost deviceHost = new ServiceHost(typeof(DeviceService.Service));
            ServiceHost calculationHost = new ServiceHost(typeof(CalculationService.Service));
            
            Console.WriteLine("List of Available Endpoints : ");

            foreach (var endpoint in deviceHost.Description.Endpoints)
            {
                Console.WriteLine("\t- Binding : {0}  |  Address : {1}", endpoint.Binding, endpoint.Address);
            }

            deviceHost.Open();
            calculationHost.Open();
            Console.WriteLine("Microsoft Services are listening.....\n");
            Console.WriteLine("\nPress enter to quit.....");
            Console.ReadLine();
            deviceHost.Close();
            calculationHost.Close();

            Console.WriteLine("Microsoft Services succesfully shutdown.....\n");
        }
    }
}
