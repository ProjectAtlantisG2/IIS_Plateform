using System;
using System.Runtime.Serialization;

namespace DeviceService
{
    [DataContract]
    public class Telemetry
    {
        [DataMember]
        private DateTime metricDate { get; set; }
        [DataMember]
        private DeviceType deviceType { get; set; }
        [DataMember]
        private string metricValue { get; set; }

        public Telemetry(DateTime? metricDate = null, DeviceType? deviceType = null, string metricValue = null)
        {
            if (metricDate != null) this.metricDate = (DateTime)metricDate;
            if (deviceType != null) this.deviceType = (DeviceType)deviceType;
            this.metricValue = metricValue;
        }

        public bool IsNull()
        {
            if (metricDate == null || deviceType == null || metricValue == null) return true;
            return false;
        }
        
    }
   
}