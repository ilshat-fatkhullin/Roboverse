using System;

namespace Assets.Scripts.Sensors
{
    public interface ISensors : IDisposable
    {
        public void Measure();
    }
}
