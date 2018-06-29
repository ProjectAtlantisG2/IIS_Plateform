using System.Runtime.Serialization;

namespace DeviceService
{
    [DataContract]
    public class Device
    {
        [DataMember]
        private string id { get; set; }
        [DataMember]
        private string name { get; set; }
        [DataMember]
        private DeviceType deviceType { get; set; }

        public Device(string id = null, string name = null, DeviceType? deviceType = null)
        {
            if (deviceType != null) this.deviceType = (DeviceType)deviceType;
            this.id = id;
            this.name = name;
        }
        
        public bool IsNull()
        {
            if (id == null) return true;
            return false;
        }

    }
}