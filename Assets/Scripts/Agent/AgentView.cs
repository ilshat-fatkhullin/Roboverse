using Assets.Scripts.Bindings;
using Assets.Scripts.Sensors;
using UnityEngine;

namespace Assets.Scripts.Agent
{
    public sealed class AgentView : MonoBehaviour, IAgentView
    {
        public IGameObject GameObject => new GameObjectBinding(gameObject);

        public ISensorsView SensorsView => _sensorsView;        

        [SerializeField]
        private SensorsView _sensorsView;
    }
}
