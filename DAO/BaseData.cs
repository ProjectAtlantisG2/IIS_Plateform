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
        public string Delay { get; set; }

        [DataMember]
        protected string DeviceType { get; set; }

        public BaseData(Delay delay, DeviceTypes deviceType)
        {
            this.Delay = delay.ToString();
            this.DeviceType = deviceType.ToString();
        }

        public string ToCollectionName(CalculType calculType)
        {
            Enum.TryParse(this.DeviceType, out DeviceTypes type);
            var DataType = DeviceTypeToDataType(type);
            if (DataType != null)
                return DataType + calculType + this.Delay;
            return null;
        }

        protected string DeviceTypeToDataType(DeviceTypes deviceType)
        {
            switch (deviceType)
            {
                case DeviceTypes.atmosphericPressureSensor:
                    {
                        return "Pres";
                    }
                case DeviceTypes.brightnessSensor:
                    {
                        return "Brig";
                    }
                case DeviceTypes.co2Sensor:
                    {
                        return "Co20";
                    }
                case DeviceTypes.humiditySensor:
                    {
                        return "Humi";
                    }
                case DeviceTypes.temperatureSensor:
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
