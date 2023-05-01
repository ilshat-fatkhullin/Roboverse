using Assets.Scripts.Sensors.Camera;
using Assets.Scripts.Sensors.Lidar;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Sensors
{
    public sealed class Sensors : ISensors
    {
        public IReadOnlyCollection<ISensor> SensorList { get; }

        public IReadOnlyCollection<ICamera> CameraList { get; }

        public IReadOnlyCollection<ILidar> LidarList { get; }

        private uint _seq = 0;

        public Sensors(
            ICameraBuilder cameraBuilder,
            ILidarBuilder lidarBuilder,
            ISensorsView view,
            ISensorsSettings settings)
        {
            CameraList = view.CameraViews.Select(v => cameraBuilder.Build(v)).ToList();
            LidarList = view.LidarViews.Select(v => lidarBuilder.Build(v, settings.LidarSettings)).ToList();

            List<ISensor> sensorList = new();
            sensorList.AddRange(CameraList);
            sensorList.AddRange(LidarList);

            SensorList = sensorList;
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