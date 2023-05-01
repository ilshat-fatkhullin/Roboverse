using Assets.Scripts.Bridge.Ros;
using System;

namespace Assets.Scripts.UI.Views.Bridges
{
    public sealed class RosBridgeView : View, IDisposable
    {
        private readonly IRosSettings _settings;

        private readonly IRoboverseField<string> _ipAddress;

        private readonly IRoboverseField<int> _port;

        public RosBridgeView(
            IRosBridge bridge,
            IRoboversePanel parent,
            IUserInterfacePrefabs prefabs) : base(parent, prefabs, bridge.Name)
        {
            _settings = bridge.Settings;

            _ipAddress = Panel.AddField(
                prefabs,
                "IP address",
                () => _settings.IpAddress,
                (value) => _settings.IpAddress = value);
            _settings.IpAddressChanged += Settings_IpAddressChanged;

            _port = Panel.AddField(
                prefabs,
                "Port",
                () => _settings.Port,
                (value) => _settings.Port = value);
            _settings.PortChanged += Settings_PortChanged;
        }

        public override void Dispose()
        {
            base.Dispose();

            _settings.IpAddressChanged -= Settings_IpAddressChanged;
            _settings.PortChanged -= Settings_PortChanged;
        }

        private void Settings_IpAddressChanged(object sender, string value)
        {
            _ipAddress.Value = value;   
        }

        private void Settings_PortChanged(object sender, int value)
        {
            _port.Value = value;
        }
    }
}
