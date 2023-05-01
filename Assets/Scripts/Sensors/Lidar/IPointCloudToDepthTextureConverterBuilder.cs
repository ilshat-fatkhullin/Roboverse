namespace Assets.Scripts.Sensors.Lidar
{
    public interface IPointCloudToDepthTextureConverterBuilder
    {
        public IPointCloudToDepthTextureConverter Build(ILidarView view, IPointCloudRenderer renderer, ILidarSettings settings);
    }
}
