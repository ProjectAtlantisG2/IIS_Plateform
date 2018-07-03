using System.Runtime.Serialization;
using DAO;

namespace CalculationService
{
    [DataContract]
    public class RawDataRequest : BaseData
    {
        [DataMember]
        public string From { get; set; }
        [DataMember]
        private string To { get; set; }

        public RawDataRequest(Delay delay, DeviceTypes deviceType, string from, string to)
            : base(delay, deviceType)
        {
            this.From = from;
            this.To = to;
        }
    }
}