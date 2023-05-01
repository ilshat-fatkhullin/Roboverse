using Assets.Scripts.Bridge.Ros;

namespace Assets.Scripts.Sensors.Lidar
{
    public sealed class LidarBuilder : ILidarBuilder
    {
        private readonly IRosBridge _rosBridge;

        private readonly IPointCloudRendererBuilder _pointCloudRendererBuilder;

        private readonly IPointCloudToDepthTextureConverterBuilder _pointToDepthTextureConverterBuilder;

        public LidarBuilder(
            IRosBridge rosBridge,
            IPointCloudRendererBuilder pointCloudRendererBuilder,
            IPointCloudToDepthTextureConverterBuilder pointCloudToDepthTextureConverterBuilder)
        {
            _rosBridge = rosBridge;
            _pointCloudRendererBuilder = pointCloudRendererBuilder;
            _pointToDepthTextureConverterBuilder = pointCloudToDepthTextureConverterBuilder;
        }

        public ILidar Build(ILidarView view, ILidarSettings settings) => new Lidar(
            view,
            _rosBridge,
            settings,
            _pointCloudRendererBuilder,
            _pointToDepthTextureConverterBuilder);
    }
}
