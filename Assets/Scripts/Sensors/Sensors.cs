using Assets.Scripts.Bridge.Ros;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Sensors
{
    public sealed class Sensors : IDisposable
    {
        public List<ISensor> SensorList { get; }

        public List<Camera.Camera> CameraList { get; }

        public List<Lidar.Lidar> LidarList { get; }

        private uint _seq = 0;

        public Sensors(
            ISensorsView view,
            IRosBridge bridge,
            SensorsSettings settings)
        {
            CameraList = view.CameraViews.Select(v => new Camera.Camera(v, bridge)).ToList();
            LidarList = view.LidarViews.Select(v => new Lidar.Lidar(v, bridge, settings.LidarSettings)).ToList();

            SensorList = new List<ISensor>();
            SensorList.AddRange(CameraList);
            SensorList.AddRange(LidarList);
        }

        public void Dispose()
        {
            foreach (ISensor sensor in SensorList)
            {
                sensor.Dispose();
            }
        }

        public void Measure()
        {
            foreach (ISensor sensor in SensorList)
            {
                sensor.Send(_seq);
            }

            _seq++;
        }
    }
}