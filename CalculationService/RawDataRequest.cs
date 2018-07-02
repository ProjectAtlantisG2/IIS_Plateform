using System.Runtime.Serialization;
using DAO;

namespace CalculationService
{
    [DataContract]
    public class RawDataRequest : BaseData
    {
        [DataMember]
        public int From { get; set; }
        [DataMember]
        private int To { get; set; }

        public RawDataRequest(Delay delay, DeviceType deviceType, int from, int to)
            : base(delay, deviceType)
        {
            this.From = from;
            this.To = to;
        }
    }
}