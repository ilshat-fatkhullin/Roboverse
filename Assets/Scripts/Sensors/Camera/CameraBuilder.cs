using Assets.Scripts.Bridge.Ros;

namespace Assets.Scripts.Sensors.Camera
{
    public sealed class CameraBuilder : ICameraBuilder
    {
        private readonly IRosBridge _rosBridge;

        public CameraBuilder(IRosBridge rosBridge) 
        { 
            _rosBridge = rosBridge;
        }

        public ICamera Build(ICameraView view) => new Camera(view, _rosBridge);
    }
}
