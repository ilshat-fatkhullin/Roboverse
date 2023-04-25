using System;

namespace Assets.Scripts.Bridge
{
    public interface IPublisher<T>
    {
        public string Topic { get; }

        public void Publish(Func<T> messageFactory);
    }
}
