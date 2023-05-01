using Assets.Scripts.Bridge.Kafka;
using System;

namespace Assets.Scripts.UI.Views.Bridges
{
    public sealed class KafkaBridgeView : View, IDisposable
    {
        private readonly IKafkaSettings _settings;

        private readonly IRoboverseField<string> _bootstrapServers;

        public KafkaBridgeView(
            IKafkaBridge bridge,
            IRoboversePanel parent,
            IUserInterfacePrefabs prefabs) : base(parent, prefabs, bridge.Name)
        {
            _settings = bridge.Settings;

            _bootstrapServers = Panel.AddField(
                prefabs,
                "Bootstrap servers",
                () => _settings.BootstrapServers,
                (value) => _settings.BootstrapServers = value);
            _settings.BootstrapServersChanged += Settings_BootstrapServersChanged;
        }

        public override void Dispose()
        {
            base.Dispose();

            _settings.BootstrapServersChanged -= Settings_BootstrapServersChanged;
        }

        private void Settings_BootstrapServersChanged(object sender, string value)
        {
            _bootstrapServers.Value = value;
        }
    }
}
