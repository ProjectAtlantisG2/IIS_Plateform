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
        private string deviceType { get; set; }

        public Device(string id = null, string name = null, string deviceType = null)
        {
            if (deviceType != null) this.deviceType = deviceType;
            this.id = id;
            this.name = name;
        }


    }
}