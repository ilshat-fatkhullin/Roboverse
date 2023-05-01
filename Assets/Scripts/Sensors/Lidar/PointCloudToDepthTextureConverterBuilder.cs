namespace Assets.Scripts.Sensors.Lidar
{
    public sealed class PointCloudToDepthTextureConverterBuilder : IPointCloudToDepthTextureConverterBuilder
    {
        public IPointCloudToDepthTextureConverter Build(ILidarView view, IPointCloudRenderer renderer, ILidarSettings settings) => new PointCloudToDepthTextureConverter(
            view.Measurements,
            view.RaysCount,
            view.Camera.farClipPlane,
            renderer.PointCloudBuffer,
            settings.PointCloudToDepthTextureConverterShader);
    }
}
