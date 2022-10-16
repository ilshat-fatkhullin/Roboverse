using UnityEngine;
using UnityEngine.VFX;

namespace Assets.Scripts.Sensors.Lidar
{
    public class LidarView : MonoBehaviour, ILidarView
    {
        public GameObject GameObject => gameObject;

        public string Topic => _topic;

        public UnityEngine.Camera Camera => _camera;

        public int Width => _width;

        public int Height => _height;

        public Vector2 FieldOfView => _fieldOfView;

        public VisualEffect VisualEffect => _visualEffect;

        [SerializeField]
        private UnityEngine.Camera _camera;

        [SerializeField]
        private string _topic;

        [SerializeField]
        private int _width;

        [SerializeField]
        private int _height;

        [SerializeField]
        private Vector2 _fieldOfView;

        [SerializeField]
        private VisualEffect _visualEffect;
    }
}