using Assets.Scripts.Sensors;
using UnityEngine;

namespace Assets.Scripts.Agent
{
    public sealed class AgentView : MonoBehaviour, IAgentView
    {
        public GameObject GameObject => gameObject;

        public ISensorsView SensorsView => _sensorsView;        

        [SerializeField]
        private SensorsView _sensorsView;
    }
}
