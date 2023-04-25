using System;

namespace Assets.Scripts.Bridge
{
    public interface ISubscriber<T>
    {
        public string Topic { get; }

        public event EventHandler<T> MessageArrived;
    }
}
