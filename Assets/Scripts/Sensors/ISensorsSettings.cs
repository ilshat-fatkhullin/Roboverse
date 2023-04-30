using Assets.Scripts.Sensors.Camera;
using Assets.Scripts.Sensors.Lidar;

namespace Assets.Scripts.Sensors
{
    public interface ISensorsSettings
    {
        public ICameraSettings CameraSettings { get; }

        public ILidarSettings LidarSettings { get; }
    }
}
