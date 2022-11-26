using Assets.Scripts.Bridge;
using Assets.Scripts.Sensors.DatasetGeneration;
using RosMessageTypes.Sensor;
using UnityEngine;

namespace Assets.Scripts.Sensors.Lidar
{
    public sealed class Lidar : ISensor
    {
        public string Topic => _view.Topic;

        public bool IsGeneratingDataset 
        { 
            get => _view.IsGeneratingDataset; 
            set => _view.IsGeneratingDataset = value; 
        }

        public ISensetiveArea SensetiveArea { get; }

        private readonly ILidarView _view;

        private readonly PointCloudRenderer _renderer;

        private readonly IPublisher<PointCloudMsg> _publisher;

        private readonly ImageDataset _dataset;

        private readonly PointCloudToDepthTextureConverter _pointCloudToDepthTextureConverter;

        public Lidar(
            ILidarView view,
            IBridge bridge,
            LidarSettings settings)
        {
            _view = view;
            _view.Camera.aspect = 1;

            _renderer = new PointCloudRenderer(
                _view.Camera,
                _view.Measurements,
                _view.RaysCount,
                _view.VerticalAngle, 
                settings.RaysToPointCloudConverterShader,
                view.VisualEffect);
            _publisher = bridge.CreatePublisher<PointCloudMsg>(Topic);
            _dataset = new ImageDataset(Topic);
            _pointCloudToDepthTextureConverter = new PointCloudToDepthTextureConverter(
                _view.Measurements,
                _view.RaysCount,
                _view.Camera.farClipPlane,
                _renderer.PointCloudBuffer,
                settings.PointCloudToDepthTextureConverterShader);
            SensetiveArea = new LidarSensetiveArea(this);
        }        

        public void Dispose()
        {
            _renderer.Dispose();
            _pointCloudToDepthTextureConverter.Dispose();
            Object.Destroy(_view.GameObject);            
        }

        public void Send(uint seq)
        {
            (Vector4[] pointCloud, GraphicsBuffer pointCloudBuffer) = _renderer.Render();
            PointCloudMsg message = PointCloudMessageBuilder.Build(seq, pointCloud);
            _publisher.Publish(() => message);

            if (IsGeneratingDataset)
            {
                Texture2D texture = _pointCloudToDepthTextureConverter.Convert();
                byte[] data = texture.EncodeToJPG();
                _dataset.AddImage(seq, data);
            }
        }
    }
}
