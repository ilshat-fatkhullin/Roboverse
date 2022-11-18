using Assets.Scripts.Sensors;
using System;
using UnityEngine;

namespace Assets.Scripts.Agent
{
    [Serializable]
    [CreateAssetMenu(fileName = "AgentSettings", menuName = "Settings/Agent", order = 1)]
    public sealed class AgentSettings : ScriptableObject
    {
        public SensorsSettings SensorsSettings;
    }
}
