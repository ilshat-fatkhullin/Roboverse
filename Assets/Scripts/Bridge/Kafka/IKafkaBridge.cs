namespace Assets.Scripts.Bridge.Kafka
{
    public interface IKafkaBridge : IBridge
    {
        public IPublisher<T> CreatePublisher<T>(string topic);

        public ISubscriber<T> CreateSubscriber<T>(string topic);
    }
}
