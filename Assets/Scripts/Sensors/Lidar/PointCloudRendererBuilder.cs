namespace Assets.Scripts.Sensors.Lidar
{
    public class PointCloudRendererBuilder : IPointCloudRendererBuilder
    {
        public IPointCloudRenderer Build(ILidarView view, ILidarSettings settings) => new PointCloudRenderer(
            view.Camera,
            view.Measurements,
            view.RaysCount,
            view.VerticalAngle,
            settings.RaysToPointCloudConverterShader,
            view.VisualEffect);
    }
}
