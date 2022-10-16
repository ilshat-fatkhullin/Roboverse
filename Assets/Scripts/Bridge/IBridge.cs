using System;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace Assets.Scripts.Bridge
{
    public interface IBridge : IDisposable
    {
        public IPublisher<T> CreatePublisher<T>(string topic) where T : Message;

        public ISubscriber<T> CreateSubscriber<T>(string topic) where T : Message;
    }
}
