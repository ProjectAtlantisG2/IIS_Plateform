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
            var DataType = DeviceTypeToDataType(this.DeviceType);
            if (DataType != null)
                return DataType + this.CalculType + this.Delay;
            return null;
        }

        private string DeviceTypeToDataType(DeviceType deviceType)
        {
            switch (deviceType)
            {
                case DeviceType.atmosphericPressureSensor:
                    {
                        return "Pres";
                    }
                case DeviceType.brightnessSensor:
                    {
                        return "Brig";
                    }
                case DeviceType.co2Sensor:
                    {
                        return "Co20";
                    }
                case DeviceType.humiditySensor:
                    {
                        return "Humi";
                    }
                case DeviceType.temperatureSensor:
                    {
                        return "Temp";
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}