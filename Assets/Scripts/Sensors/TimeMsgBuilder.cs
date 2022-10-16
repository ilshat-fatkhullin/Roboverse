using RosMessageTypes.BuiltinInterfaces;
using System;
using UnityEngine;

namespace Assets.Scripts.Sensors
{
    public static class TimeMsgBuilder
    {
        public static TimeMsg Build()
        {
            uint sec = Convert.ToUInt32(Mathf.FloorToInt(Time.fixedTime));
            uint nanosec = Convert.ToUInt32(Mathf.RoundToInt((Time.fixedTime - sec) * 1000000000));
            
            return new (sec, nanosec);
        }
    }
}
