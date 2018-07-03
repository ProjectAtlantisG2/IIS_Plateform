using System;
using System.Runtime.Serialization;
using DAO;

namespace CalculationService
{
    [DataContract]
    public class ComplexDataRequest : ComplexData
    {
        [DataMember]
        public string From { get; set; }
        [DataMember]
        public string To { get; set; }

        public ComplexDataRequest(Delay delay, CalculType calculType, DeviceTypes deviceType, string from, string to)
            : base(delay, calculType, deviceType)
        {
            if (from != null) this.From = from;
            if (from != null) this.To = to;
        }

        public string ToCollectionName()
        {
            Enum.TryParse(this.DeviceType, out DeviceTypes type);
            var DataType = base.DeviceTypeToDataType(type);
            if (DataType != null)
                return DataType + this.CalculType + this.Delay;
            return null;
        }

    }
}