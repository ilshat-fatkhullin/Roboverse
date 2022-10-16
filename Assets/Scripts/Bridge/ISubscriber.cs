using System;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace Assets.Scripts.Bridge
{
    public interface ISubscriber<Ros> : IDisposable where Ros : Message
    {
        public string Topic { get; }

        public event EventHandler<Ros> RosMessageArrived;
    }
}
