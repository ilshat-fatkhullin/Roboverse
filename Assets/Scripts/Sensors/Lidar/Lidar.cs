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

        private readonly PointCloudRenderer _renderer;

        private readonly IPublisher<PointCloudMsg> _publisher;

        private readonly PointCloudToDepthTextureConverter _pointCloudToDepthTextureConverter;

        public Lidar(
            ILidarView view,
            IRosBridge bridge,
            ILidarSettings settings)
        {
            _view = view;
            _view.Camera.aspect = 1;

            _renderer = new(
                view.Camera,
                view.Measurements,
                view.RaysCount,
                view.VerticalAngle,
                settings.RaysToPointCloudConverterShader,
                view.VisualEffect);

            _pointCloudToDepthTextureConverter = new(
                view.Measurements,
                view.RaysCount,
                view.Camera.farClipPlane,
                _renderer.PointCloudBuffer,
                settings.PointCloudToDepthTextureConverterShader);

            _publisher = bridge.CreatePublisher<PointCloudMsg>(Topic);
        }

        public void Send(uint seq)
        {
            (Vector4[] pointCloud, GraphicsBuffer pointCloudBuffer) = _renderer.Render();
            PointCloudMsg message = PointCloudMessageBuilder.Build(seq, pointCloud);
            _publisher.Publish(() => message);
        }

        public void Dispose()
        {
            _renderer.Dispose();
            _pointCloudToDepthTextureConverter.Dispose();
        }
    }
}
