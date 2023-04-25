using Assets.Scripts.Bridge.Kafka;
using Assets.Scripts.Bridge.Ros;
using UnityEngine;

namespace Assets.Scripts.Bridge
{
    [CreateAssetMenu(fileName = "BridgeSettings", menuName = "Settings/Bridge", order = 1)]
    public sealed class BridgeSettings : ScriptableObject
    {
        public RosSettings RosSettings;

        public KafkaSettings KafkaSettings;
    }
}
