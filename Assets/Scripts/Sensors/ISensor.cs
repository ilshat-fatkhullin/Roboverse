using System;

namespace Assets.Scripts.Sensors
{
    public interface ISensor : IDisposable
    {
        public string Topic { get; }

        public void Send(uint seq);
    }
}
