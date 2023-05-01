using Assets.Scripts.Sensors.Camera;
using Assets.Scripts.Sensors.Lidar;

namespace Assets.Scripts.Sensors
{
    public sealed class SensorsBuilder : ISensorsBuilder
    {
        private readonly ICameraBuilder _cameraBuilder;

        private readonly ILidarBuilder _lidarBuilder;

        public SensorsBuilder(
            ICameraBuilder cameraBuilder,
            ILidarBuilder lidarBuilder) 
        {
            _cameraBuilder = cameraBuilder;
            _lidarBuilder = lidarBuilder;
        }

        public ISensors Build(ISensorsView view, ISensorsSettings settings) => new Sensors(
            _cameraBuilder,
            _lidarBuilder,
            view,
            settings);
    }
}
