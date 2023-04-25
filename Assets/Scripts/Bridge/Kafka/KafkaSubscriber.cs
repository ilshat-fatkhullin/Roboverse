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

        private readonly IUnityCallbacks _callbacks;

        private readonly IConsumer<Ignore, string> _consumer;

        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private readonly ConcurrentQueue<T> _queue = new();

        public KafkaSubscriber(
            IUnityCallbacks callbacks,
            IConsumer<Ignore, string> consumer) 
        {
            _callbacks = callbacks;
            _consumer = consumer;

            Topic = consumer.Name;

            Task.Run(() =>
            {
                while (true)
                {
                    ConsumeResult<Ignore, string> consumeResult = consumer.Consume(_cancellationTokenSource.Token);
                    string message = consumeResult.Message.Value;

                    _queue.Enqueue(JsonSerializer.Deserialize<T>(message));
                }
            }, 
            _cancellationTokenSource.Token);

            _callbacks.FixedUpdateOccured += FixedUpdateOccured;
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

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            Disposed?.Invoke(this, _consumer);
        }
    }
}
