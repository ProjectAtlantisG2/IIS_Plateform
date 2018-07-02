using System.Collections.Generic;
using System.Runtime.Serialization;
using DAO;

namespace CalculationService
{
    [DataContract]
    public class RawDataResponse : RawDataRequest
    {

        [DataMember]
        public List<float> RawData { get; set; }
        
        public RawDataResponse(Delay delay, DeviceType deviceType, int from, int to, List<float> rawData) 
            : base(delay, deviceType, from, to)
        {
            this.RawData = rawData;
        }

    }
}