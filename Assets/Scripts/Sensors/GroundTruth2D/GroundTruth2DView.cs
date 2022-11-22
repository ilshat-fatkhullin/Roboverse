using UnityEngine;

namespace Assets.Scripts.Sensors.GroundTruth2D
{
    public class GroundTruth2DView : MonoBehaviour, IGroundTruth2DView
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