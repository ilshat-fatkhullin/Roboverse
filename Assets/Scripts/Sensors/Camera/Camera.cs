using Assets.Scripts.Bridge;
using Assets.Scripts.Bridge.Ros;
using Assets.Scripts.Sensors.DatasetGeneration;
using RosMessageTypes.Sensor;

namespace Assets.Scripts.Sensors.Camera
{
    public sealed class Camera : ICamera
    {
        public string Topic => _view.Topic;

        public ISensetiveArea SensetiveArea { get; }

        public int Width => _view.Width;

        public int Height => _view.Height;

        public float MaxDistance => _view.Camera.farClipPlane;

        public UnityEngine.Camera UnityCamera => _view.Camera;

        private readonly ICameraView _view;

        private readonly IPublisher<ImageMsg> _publisher;

        private readonly ImageRenderer _renderer;

        private readonly ImageMessageBuilder _messageBuilder;

        private readonly ImageDataset _dataset;

        public Camera(
            ICameraView view,
            IRosBridge bridge)
        {
            _view = view;
            _publisher = bridge.CreatePublisher<ImageMsg>(Topic);
            _renderer = new ImageRenderer(_view.Width, _view.Height, _view.Camera);
            _messageBuilder = new ImageMessageBuilder(view.Camera);
            _dataset = new ImageDataset(view.Topic);
        }

        public void Dispose()
        {
            _renderer.Dispose();        
        }

        public void Send(uint seq)
        {
            byte[] data = _renderer.RenderIntoBytes();
            _publisher.Publish(() => _messageBuilder.Build(seq, data));
        }
    }
}
