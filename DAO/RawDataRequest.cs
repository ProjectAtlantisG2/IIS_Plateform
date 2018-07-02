using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DAO
{
    [DataContract]
    public class RawDataRequest : BaseData
    {
        [DataMember]
        private int From { get; set; }
        [DataMember]
        private int To { get; set; }

        public RawDataRequest(Delay delay, DeviceType deviceType, int from, int to)
            :base(delay, deviceType)
        {
            this.From = from;
            this.To = to;
        }
    }
}
