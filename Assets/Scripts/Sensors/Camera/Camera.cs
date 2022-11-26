using Assets.Scripts.Bridge;
using Assets.Scripts.Sensors.DatasetGeneration;
using RosMessageTypes.Sensor;

namespace Assets.Scripts.Sensors.Camera
{
    public sealed class Camera : ISensor
    {
        public string Topic => _view.Topic;

        public ISensetiveArea SensetiveArea { get; }

        public bool IsGeneratingDataset
        {
            get => _view.IsGeneratingDataset;
            set => _view.IsGeneratingDataset = value;
        }

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
            IBridge bridge)
        {
            _view = view;
            _publisher = bridge.CreatePublisher<ImageMsg>(Topic);
            _renderer = new ImageRenderer(_view.Width, _view.Height, _view.Camera);
            _messageBuilder = new ImageMessageBuilder(view.Camera);
            _dataset = new ImageDataset(view.Topic);

            SensetiveArea = new CameraSensetiveArea(this);
        }

        public void Dispose()
        {
            _renderer.Dispose();
            UnityEngine.Object.Destroy(_view.GameObject);            
        }

        public void Send(uint seq)
        {
            byte[] data = _renderer.RenderIntoBytes();
            _publisher.Publish(() => _messageBuilder.Build(seq, data));

            if (IsGeneratingDataset)
            {
                _dataset.AddImage(seq, data);
            }
        }
    }
}
