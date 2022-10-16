using UnityEngine;

namespace Assets.Scripts.Sensors.Camera
{
    public class CameraView : MonoBehaviour, ICameraView
    {
        public string Topic => _topic;

        public GameObject GameObject => gameObject;

        public UnityEngine.Camera Camera => _camera;        

        [SerializeField]
        private UnityEngine.Camera _camera;

        [SerializeField]
        private string _topic;
    }
}