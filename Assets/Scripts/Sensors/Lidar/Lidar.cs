namespace Assets.Scripts.Sensors.Lidar
{
    public sealed class Lidar : ISensor
    {
        public string Topic => _view.Topic;

        private readonly ILidarView _view;

        private readonly PointCloudMessageBuilder _messageBuilder;

        public Lidar(ILidarView view, LidarSettings settings)
        {
            _view = view;
            _view.Camera.aspect = 1;

            _messageBuilder = new(
                _view.Camera, 
                _view.Resolution, 
                _view.Measurements,
                _view.RaysCount,
                _view.VerticalAngle, 
                settings.Shader, 
                view.VisualEffect);
        }        

        public void Dispose()
        {
            _messageBuilder.Dispose();
            UnityEngine.Object.Destroy(_view.GameObject);            
        }

        public void Send(uint seq)
        {
            _messageBuilder.Build(seq);
        }
    }
}
