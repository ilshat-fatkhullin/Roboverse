using Assets.Scripts.Sensors.Camera;
using Assets.Scripts.Sensors.Lidar;
using System.Collections.Generic;

namespace Assets.Scripts.Sensors
{
    public interface ISensorsView : IView
    {
        public IEnumerable<ICameraView> CameraViews { get; }

        public IEnumerable<ILidarView> LidarViews { get; }
    }
}