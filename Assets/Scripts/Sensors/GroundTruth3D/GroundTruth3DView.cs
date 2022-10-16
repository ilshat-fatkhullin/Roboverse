using UnityEngine;

namespace Assets.Scripts.Sensors.GroundTruth3D
{
    public class GroundTruth3DView : MonoBehaviour, IGroundTruth3DView
    {
        public GameObject GameObject => gameObject;

        public string Topic => _topic;

        [SerializeField]
        private string _topic;
    }
}