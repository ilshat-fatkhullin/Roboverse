using UnityEngine;

namespace Assets.Scripts.Sensors.GroundTruth3D
{
    public class GroundTruth3DView : MonoBehaviour, IGroundTruth3DView
    {
        public GameObject GameObject => gameObject;

        public string Topic => _topic;

        public bool IsGeneratingDataset
        {
            get => _isGeneratingDataset;
            set => _isGeneratingDataset = value;
        }

        [SerializeField]
        private string _topic;

        [SerializeField]
        private bool _isGeneratingDataset;
    }
}