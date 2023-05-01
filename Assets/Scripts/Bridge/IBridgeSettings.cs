using Assets.Scripts.Bridge.Kafka;
using Assets.Scripts.Bridge.Ros;

namespace Assets.Scripts.Bridge
{
    public interface IBridgeSettings
    {
        public IRosSettings RosSettings { get; }

        public IKafkaSettings KafkaSettings { get; }
    }
}
