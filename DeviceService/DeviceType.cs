using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeviceService
{
    public enum DeviceType
    {
        presenceSensor,
        temperatureSensor,
        brightnessSensor,
        atmosphericPressureSensor,
        humiditySensor,
        soundLevelSensor,
        gpsSensor,
        co2Sensor,
        ledDevice,
        beeperDevice
    }
}