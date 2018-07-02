using System;
using DAO;
using CalculationService;
using System.Linq;
using System.Configuration;

namespace CalculationEngine
{
    class Program
    {
        private static readonly string mongoConnectionString = ConfigurationManager.AppSettings["MongoConnectionString"];
        private static readonly string mongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"];
        private static readonly string mongoCollectionName = ConfigurationManager.AppSettings["MongoCollectionName"];
        private static Service comm = new Service();
        public static void Main(string[] args)
        {

        }

        private void HourlyCalculation()
        {
            ComplexCalculation(Delay.Hour);
        }

        private void DaylyCalculation()
        {
            ComplexCalculation(Delay.Day);
        }

        private void ComplexCalculation(Delay delay)
        {
            MongoConnection mongoConnection = new MongoConnection(mongoConnectionString, mongoDatabaseName);
            foreach (DeviceType type in Enum.GetValues(typeof(CalculatedDeviceType)))
            {
                RawDataResponse response = comm.GetDataForCalculation(new RawDataRequest(delay, type, DateTime.Now.Hour - 1, DateTime.Now.Hour)).Result;

                var max = response.RawData.Max();
                var min = response.RawData.Min();
                var moy = response.RawData.Sum() / response.RawData.Count;

                //mongoConnection.InsertData(response.ToCollectionName(CalculType.Max), max, response.From);
                //mongoConnection.InsertData(response.ToCollectionName(CalculType.Min), min, response.From);
                //mongoConnection.InsertData(response.ToCollectionName(CalculType.Moy), moy, response.From);
            };
        } 
    }
}
