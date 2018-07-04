using System;
using DAO;
using CalculationService;
using System.Linq;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;

namespace CalculationEngine
{
    class Program
    {
        private static readonly string mongoConnectionString = ConfigurationManager.AppSettings["MongoConnectionString"];
        private static readonly string mongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"];
        private static Service comm = new Service();
        private static CultureInfo provider = CultureInfo.InvariantCulture;

        public static void Main(string[] args)
        {
            Stopwatch hourlyTimer = new Stopwatch();
            Stopwatch daylyTimer = new Stopwatch();

            //TEST
            MongoConnection mongoConnection = new MongoConnection(mongoConnectionString, mongoDatabaseName);
            ComplexCalculation(Delay.Hour);

            //IN APP TIMER
            //while (true)
            //{
            //    if(hourlyTimer.Elapsed > TimeSpan.FromHours(1))
            //    {
            //        hourlyTimer.Restart();
            //        ComplexCalculation(Delay.Hour);
            //    }
            //    if(daylyTimer.Elapsed < TimeSpan.FromDays(1))
            //    {
            //        daylyTimer.Restart();
            //        ComplexCalculation(Delay.Day);
            //    }
            //}

            //TASK SCHEDULER
            //foreach (var arg in args)
            //{
            //    if (arg == "Hour")
            //    {
            //        ComplexCalculation(Delay.Hour);
            //    }
            //    if (arg == "Day")
            //    {
            //        ComplexCalculation(Delay.Day);
            //    }
            //}
            //Console.ReadLine();
        }

        private static void ComplexCalculation(Delay delay)
        {
            MongoConnection mongoConnection = new MongoConnection(mongoConnectionString, mongoDatabaseName);
            foreach (DeviceTypes type in Enum.GetValues(typeof(CalculatedDeviceType)))
            {
                RawDataResponse response = comm.GetDataForCalculation(new RawDataRequest(delay, type, DateTime.Now.AddHours(-1).ToString("dd/MM/yyyy HH:mm:ss:fff"), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff"))).Result;

                var max = response.RawData.Max();
                var min = response.RawData.Min();
                var moy = response.RawData.Sum() / response.RawData.Count;

                mongoConnection.InsertData(response.ToCollectionName(CalculType.Max), max, DateTime.ParseExact(response.From, "dd/MM/yyyy HH:mm:ss:fff", provider), delay);
                mongoConnection.InsertData(response.ToCollectionName(CalculType.Min), min, DateTime.ParseExact(response.From, "dd/MM/yyyy HH:mm:ss:fff", provider), delay);
                mongoConnection.InsertData(response.ToCollectionName(CalculType.Moy), moy, DateTime.ParseExact(response.From, "dd/MM/yyyy HH:mm:ss:fff", provider), delay);
            };
        } 
    }
}
