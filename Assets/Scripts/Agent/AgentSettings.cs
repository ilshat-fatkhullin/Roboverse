using Assets.Scripts.Sensors;
using System;
using UnityEngine;

namespace Assets.Scripts.Agent
{
    [Serializable]
    [CreateAssetMenu(fileName = "AgentSettings", menuName = "Settings/Agent", order = 1)]
    public sealed class AgentSettings : ScriptableObject, IAgentSettings
    {
        public ISensorsSettings SensorsSettings => _sensorsSettings;

        [SerializeField]
        private SensorsSettings _sensorsSettings;
    }
}
