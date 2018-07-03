using System;
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
        
        public RawDataResponse(Delay delay, DeviceTypes deviceType, string from, string to, List<float> rawData) 
            : base(delay, deviceType, from, to)
        {
            if(rawData != null) this.RawData = rawData;
        }

    }
}