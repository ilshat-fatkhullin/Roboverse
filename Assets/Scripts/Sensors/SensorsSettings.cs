using Assets.Scripts.Sensors.Camera;
using Assets.Scripts.Sensors.Lidar;
using System;

namespace Assets.Scripts.Sensors
{
    [Serializable]
    public sealed class SensorsSettings
    {
        public CameraSettings CameraSettings;

        public LidarSettings LidarSettings;
    }
}
