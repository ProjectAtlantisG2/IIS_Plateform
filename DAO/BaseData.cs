using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DAO
{
    [DataContract]
    public class BaseData
    {
        [DataMember]
        protected Delay Delay { get; set; }

        [DataMember]
        protected DeviceType DeviceType { get; set; }

        public BaseData(Delay delay, DeviceType deviceType)
        {
            this.Delay = delay;
            this.DeviceType = deviceType;
        }
    }
}
