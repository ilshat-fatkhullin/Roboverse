using UnityEngine;
using UnityEngine.VFX;

namespace Assets.Scripts.Sensors.Lidar
{
    public class LidarView : MonoBehaviour, ILidarView
    {
        public GameObject GameObject => gameObject;

        public string Topic => _topic;

        public UnityEngine.Camera Camera => _camera;

        public VisualEffect VisualEffect => _visualEffect;

        public int Measurements => _measurements;

        public int RaysCount => _raysCount;

        public float VerticalAngle => _verticalAngle;

        public bool IsGeneratingDataset
        {
            get => _isGeneratingDataset;
            set => _isGeneratingDataset = value;
        }

        [SerializeField]
        private UnityEngine.Camera _camera;

        [SerializeField]
        private string _topic;

        [SerializeField]
        private int _measurements;

        [SerializeField]
        private int _raysCount;

        [SerializeField]
        private float _verticalAngle;

        [SerializeField]
        private bool _isGeneratingDataset;

        [SerializeField]
        private VisualEffect _visualEffect;
    }
}