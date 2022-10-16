using Assets.Scripts.Bridge;
using RosMessageTypes.Sensor;

namespace Assets.Scripts.Sensors.Camera
{
    public sealed class Camera : ISensor
    {
        public string Topic => _view.Topic;

        private readonly ICameraView _view;

        private readonly IPublisher<ImageMsg> _publisher;

        private readonly ImageMessageBuilder _messageBuilder;

        public Camera(
            ICameraView view,
            IBridge bridge)
        {
            _view = view;
            _publisher = bridge.CreatePublisher<ImageMsg>(Topic);
            _messageBuilder = new ImageMessageBuilder(view.Camera);
        }

        public void Dispose()
        {
            _messageBuilder.Dispose();
            UnityEngine.Object.Destroy(_view.GameObject);            
        }

        public void Send(uint seq)
        {
            _publisher.Publish(() => _messageBuilder.Build(seq));
        }
    }
}
