using System;
using System.Threading.Tasks;
using DAO;
using CalculationService;

namespace CalculationEngine
{
    class Program
    {
        public static Service comm = new Service();
        static void Main(string[] args)
        {

        }

        public void HourlyCalculation()
        {
            Parallel.ForEach((DeviceType[])Enum.GetValues(typeof(CalculatedDeviceType)), type =>
            {
                comm.GetDataForCalculation(new RawDataRequest(Delay.Hour, type,DateTime.Now.Hour, DateTime.Now.Hour - 1));
            });
        }
        


        public void Dailycalculation()
        {

        }
    }
}
