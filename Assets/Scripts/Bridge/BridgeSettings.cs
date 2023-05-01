using Assets.Scripts.Bridge.Kafka;
using Assets.Scripts.Bridge.Ros;
using UnityEngine;

namespace Assets.Scripts.Bridge
{
    [CreateAssetMenu(fileName = "BridgeSettings", menuName = "Settings/Bridge", order = 1)]
    public sealed class BridgeSettings : ScriptableObject, IBridgeSettings
    {
        public IRosSettings RosSettings => _rosSettings;

        public IKafkaSettings KafkaSettings => _kafkaSettings;

        [SerializeField]
        private RosSettings _rosSettings;

        [SerializeField]
        private KafkaSettings _kafkaSettings;
    }
}
