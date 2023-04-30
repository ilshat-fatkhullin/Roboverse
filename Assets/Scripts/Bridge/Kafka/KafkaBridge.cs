using Assets.Scripts.Settings;
using Confluent.Kafka;

namespace Assets.Scripts.Bridge.Kafka
{
    public sealed class KafkaBridge : IKafkaBridge
    {
        public string Name => "Kafka";

        public ISettings Settings { get; }

        private readonly IUnityCallbacks _callbacks;

        private readonly ConsumerConfig _consumerConfig;

        private readonly IProducer<Null, string> _producer;

        public KafkaBridge(
            KafkaSettings settings,
            IUnityCallbacks callbacks)
        {
            Settings = settings;
            _callbacks = callbacks;

            _consumerConfig = new()
            {
                BootstrapServers = settings.BootstrapServers,
                GroupId = "simulator",
                AutoOffsetReset = AutoOffsetReset.Latest,
            };

            ProducerConfig producerConfig = new()
            {
                BootstrapServers = settings.BootstrapServers
            };

            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }

        public IPublisher<T> CreatePublisher<T>(string topic) => new KafkaPublisher<T>(topic, _producer);

        public ISubscriber<T> CreateSubscriber<T>(string topic)
        {
            KafkaSubscriber<T> subscriber = new(_callbacks,  topic, _consumerConfig);
            subscriber.Disposed += Subscriber_Disposed;
            return subscriber;
        }

        public void Dispose()
        {           
            _producer.Dispose();
        }

        private void Subscriber_Disposed(object sender, IConsumer<Ignore, string> consumer)
        {
            consumer.Dispose();
        }
    }
}
