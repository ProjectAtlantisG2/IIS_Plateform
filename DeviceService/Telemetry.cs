using System;
using System.Runtime.Serialization;

namespace DeviceService
{
    [DataContract]
    public class Telemetry
    {
        [DataMember]
        public string metricDate { get; set; }
        [DataMember]
        public string deviceType { get; set; }
        [DataMember]
        public string metricValue { get; set; }

        public Telemetry(string metricDate = null, string deviceType = null, string metricValue = null)
        {
            if (metricDate != null) this.metricDate = metricDate;
            if (deviceType != null) this.deviceType = deviceType;
            this.metricValue = metricValue;
        }
        
    }
   
}