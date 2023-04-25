using System;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace Assets.Scripts.Bridge.Ros
{
    public sealed class RosPublisher<T> : IPublisher<T> where T : Message
    {
        public string Topic { get; }

        private readonly ROSConnection _connection;

        private readonly RosTopicState _topicState;

        public RosPublisher(string topic, ROSConnection connection)
        {
            Topic = topic;
            _connection = connection;
            _topicState = connection.RegisterPublisher<T>(Topic);
        }

        public void Publish(Func<T> rosMessageFactory)
        {
            if (_connection.HasConnectionError)
            {
                return;
            }

            T message = rosMessageFactory();
            _topicState.Publish(message);
        }
    }
}
