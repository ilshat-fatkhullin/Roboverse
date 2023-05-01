using Assets.Scripts.Bridge.Ros;

namespace Assets.Scripts.Sensors.Lidar
{
    public interface ILidarBuilder
    {
        public ILidar Build(ILidarView view, ILidarSettings settings);
    }
}
