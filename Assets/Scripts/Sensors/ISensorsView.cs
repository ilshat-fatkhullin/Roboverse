using Assets.Scripts.Sensors.Camera;
using Assets.Scripts.Sensors.GroundTruth2D;
using Assets.Scripts.Sensors.GroundTruth3D;
using Assets.Scripts.Sensors.Lidar;
using System.Collections.Generic;

namespace Assets.Scripts.Sensors
{
    public interface ISensorsView : IView
    {
        public IEnumerable<ICameraView> CameraViews { get; }

        public IEnumerable<IGroundTruth2DView> GroundTruth2DViews { get; }

        public IEnumerable<IGroundTruth3DView> GroundTruth3DViews { get; }

        public IEnumerable<ILidarView> LidarViews { get; }
    }
}