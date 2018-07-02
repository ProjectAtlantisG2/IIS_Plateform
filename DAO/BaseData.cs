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

        public string ToCollectionName(CalculType calculType)
        {
            var DataType = DeviceTypeToDataType(this.DeviceType);
            if (DataType != null)
                return DataType + calculType + this.Delay;
            return null;
        }

        protected string DeviceTypeToDataType(DeviceType deviceType)
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
