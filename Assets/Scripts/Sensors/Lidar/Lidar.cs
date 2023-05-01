using Assets.Scripts.Bridge;
using Assets.Scripts.Bridge.Ros;
using RosMessageTypes.Sensor;
using UnityEngine;

namespace Assets.Scripts.Sensors.Lidar
{
    public sealed class Lidar : ILidar
    {
        public string Topic => _view.Topic;

        private readonly ILidarView _view;

        private readonly IPointCloudRenderer _pointCloudRenderer;

        private readonly IPublisher<PointCloudMsg> _publisher;

        private readonly IPointCloudToDepthTextureConverter _pointCloudToDepthTextureConverter;

        public Lidar(
            ILidarView view,
            IRosBridge bridge,
            ILidarSettings settings,
            IPointCloudRendererBuilder pointCloudRendererBuilder,
            IPointCloudToDepthTextureConverterBuilder pointCloudToDepthTextureConverter)
        {
            _view = view;
            _view.Camera.aspect = 1;

            _pointCloudRenderer = pointCloudRendererBuilder.Build(view, settings);
            _pointCloudToDepthTextureConverter = pointCloudToDepthTextureConverter.Build(view, _pointCloudRenderer, settings);

            _publisher = bridge.CreatePublisher<PointCloudMsg>(Topic);
        }

        public void Send(uint seq)
        {
            (Vector4[] pointCloud, GraphicsBuffer pointCloudBuffer) = _pointCloudRenderer.Render();
            PointCloudMsg message = PointCloudMessageBuilder.Build(seq, pointCloud);
            _publisher.Publish(() => message);
        }

        public void Dispose()
        {
            _pointCloudRenderer.Dispose();
            _pointCloudToDepthTextureConverter.Dispose();
        }
    }
}
