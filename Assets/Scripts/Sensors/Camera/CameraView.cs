using Assets.Scripts.Bindings;
using UnityEngine;

namespace Assets.Scripts.Sensors.Camera
{
    public class CameraView : MonoBehaviour, ICameraView
    {
        public string Topic => _topic;

        public IGameObject GameObject => new GameObjectBinding(gameObject);

        public UnityEngine.Camera Camera => _camera;

        public bool IsGeneratingDataset
        {
            get => _isGeneratingDataset;
            set => _isGeneratingDataset = value;
        }

        public int Width => _width;

        public int Height => _height;

        [SerializeField]
        private UnityEngine.Camera _camera;

        [SerializeField]
        private string _topic;

        [SerializeField]
        private bool _isGeneratingDataset;

        [SerializeField]
        private int _width;

        [SerializeField]
        private int _height;
    }
}