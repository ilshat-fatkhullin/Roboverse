using Assets.Scripts.Bridge.Ros;

namespace Assets.Scripts.Sensors.Camera
{
    public interface ICameraBuilder
    {
        public ICamera Build(ICameraView view);
    }
}
