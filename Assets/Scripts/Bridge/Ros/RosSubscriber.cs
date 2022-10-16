using System;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace Assets.Scripts.Bridge.Ros
{
    public sealed class RosSubscriber<T> : ISubscriber<T> where T : Message
    {
        public string Topic { get; }

        public event EventHandler<T> RosMessageArrived;

        private readonly ROSConnection _connection;

        public RosSubscriber(string topic, ROSConnection connection)
        {
            Topic = topic;

            _connection = connection;
            _connection.Subscribe<T>(Topic, OnMessageArrived);
        }

        private void OnMessageArrived(T message)
        {
            RosMessageArrived?.Invoke(this, message);
        }

        public void Dispose()
        {
            _connection.Unsubscribe(Topic);
        }
    }
}
