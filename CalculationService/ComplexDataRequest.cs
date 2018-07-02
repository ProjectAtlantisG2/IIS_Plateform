using System;
using System.Runtime.Serialization;
using DAO;

namespace CalculationService
{
    [DataContract]
    public class ComplexDataRequest : ComplexData
    {
        [DataMember]
        private DateTime From { get; set; }
        [DataMember]
        private DateTime To { get; set; }

        public ComplexDataRequest(Delay delay, CalculType calculType, DeviceType deviceType, DateTime from, DateTime to)
            : base(delay, calculType, deviceType)
        {
            this.From = from;
            this.To = to;
        }

        public string ToCollectionName()
        {
            var DataType = base.DeviceTypeToDataType(this.DeviceType);
            if (DataType != null)
                return DataType + this.CalculType + this.Delay;
            return null;
        }

    }
}