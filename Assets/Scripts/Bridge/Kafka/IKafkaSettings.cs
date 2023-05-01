using System;

namespace Assets.Scripts.Bridge.Kafka
{
    public interface IKafkaSettings
    {
        public string BootstrapServers { get; set; }

        public event EventHandler<string> BootstrapServersChanged;
    }
}
