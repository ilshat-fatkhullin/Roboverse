namespace Assets.Scripts.Sensors.Lidar
{
    public interface IPointCloudRendererBuilder
    {
        public IPointCloudRenderer Build(ILidarView view, ILidarSettings settings);
    }
}
