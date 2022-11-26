using System;
using Unity.Robotics.ROSTCPConnector;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace Assets.Scripts.Bridge.Ros
{
    public sealed class RosPublisher<T> : IPublisher<T> where T : Message
    {
        public string Topic { get; }

        private readonly RosTopicState _topicState;

        public RosPublisher(string topic, ROSConnection connection)
        {
            Topic = topic;
            _topicState = connection.RegisterPublisher<T>(Topic);
        }

        public void Publish(Func<T> rosMessageFactory)
        {
            T message = rosMessageFactory();
            _topicState.Publish(message);
        }
    }
}
