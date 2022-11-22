namespace Assets.Scripts.Sensors.GroundTruth3D
{
    public sealed class GroundTruth3D : ISensor
    {
        public string Topic => _view.Topic;

        public bool IsGeneratingDataset 
        { 
            get => _view.IsGeneratingDataset; 
            set => _view.IsGeneratingDataset = value; 
        }

        private readonly IGroundTruth3DView _view;

        public GroundTruth3D(IGroundTruth3DView view)
        {
            _view = view;
        }        

        public void Dispose()
        {
            UnityEngine.Object.Destroy(_view.GameObject);
        }

        public void Send(uint seq)
        {
            
        }
    }
}
