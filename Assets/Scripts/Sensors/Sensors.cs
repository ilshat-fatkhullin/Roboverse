using Assets.Scripts.Bridge.Ros;
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

        private readonly IRosBridge _rosBridge;

        private readonly ISensorsSettings _settings;

        private uint _seq = 0;

        public Sensors(
            ISensorsView view,
            IRosBridge rosBridge,
            ISensorsSettings settings)
        {
            _rosBridge = rosBridge;
            _settings = settings;

            CameraList = view.CameraViews.Select(v => CreateCamera(v)).ToList();
            LidarList = view.LidarViews.Select(v => CreateLidar(v)).ToList();

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

        private ICamera CreateCamera(ICameraView view) => new Camera.Camera(view, _rosBridge);

        private ILidar CreateLidar(ILidarView view) => new Lidar.Lidar(view, _rosBridge, _settings.LidarSettings);
    }
}