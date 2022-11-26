using System;

namespace Assets.Scripts.Sensors
{
    public interface ISensor : IDisposable
    {
        public string Topic { get; }

        public ISensetiveArea SensetiveArea { get; }

        public bool IsGeneratingDataset { get; set; }        

        public void Send(uint seq);
    }
}
