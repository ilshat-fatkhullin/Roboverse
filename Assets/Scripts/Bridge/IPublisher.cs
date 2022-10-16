using System;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace Assets.Scripts.Bridge
{
    public interface IPublisher<Ros> where Ros : Message
    {
        public string Topic { get; }

        public void Publish(Func<Ros> rosMessageFactory);
    }
}
