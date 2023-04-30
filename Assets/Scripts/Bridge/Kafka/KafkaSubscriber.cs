using Confluent.Kafka;
using System;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Assets.Scripts.Bridge.Kafka
{
    public sealed class KafkaSubscriber<T> : ISubscriber<T>
    {
        public string Topic { get; }

        public event EventHandler<T> MessageArrived;

        public event EventHandler<IConsumer<Ignore, string>> Disposed;

        public ConsumerConfig Config 
        {
            get => _config;
            set
            {
                _config = value;
                ApplyConfigurationChange();
            }
        }

        private readonly IUnityCallbacks _callbacks;

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private readonly ConcurrentQueue<T> _queue = new();

        private IConsumer<Ignore, string> _consumer;

        private ConsumerConfig _config;

        public KafkaSubscriber(
            IUnityCallbacks callbacks,
            string topic,
            ConsumerConfig config)
        {
            _callbacks = callbacks;
            _callbacks.FixedUpdateOccured += FixedUpdateOccured;

            Topic = topic;
            Config = config;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            Disposed?.Invoke(this, _consumer);
        }

        private void ApplyConfigurationChange()
        {
            _cancellationTokenSource.Cancel();
            _consumer = new ConsumerBuilder<Ignore, string>(_config).Build();
            _consumer.Subscribe(Topic);

            Task.Run(() =>
            {
                while (true)
                {
                    ConsumeResult<Ignore, string> consumeResult = _consumer.Consume(_cancellationTokenSource.Token);
                    string message = consumeResult.Message.Value;

                    _queue.Enqueue(JsonSerializer.Deserialize<T>(message));
                }
            },
            _cancellationTokenSource.Token);
        }

        private void FixedUpdateOccured(object sender, EventArgs e)
        {
            while (!_queue.IsEmpty)
            {
                if (!_queue.TryDequeue(out T message))
                {
                    return;
                }

                MessageArrived?.Invoke(this, message);
            }
        }
    }
}
