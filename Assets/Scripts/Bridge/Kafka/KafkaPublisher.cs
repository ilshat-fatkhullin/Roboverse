using Confluent.Kafka;
using System;
using System.Text.Json;

namespace Assets.Scripts.Bridge.Kafka
{
    public sealed class KafkaPublisher<T> : IPublisher<T>
    {
        public string Topic { get; }

        private readonly IProducer<Null, string> _producer;

        public KafkaPublisher(string topic, IProducer<Null, string> producer)
        {
            Topic = topic;
            _producer = producer;
        }

        public void Publish(Func<T> messageFactory)
        {
            string message = JsonSerializer.Serialize(messageFactory());
            _producer.ProduceAsync(Topic, new Message<Null, string> { Value = message }).Wait();
        }
    }
}
