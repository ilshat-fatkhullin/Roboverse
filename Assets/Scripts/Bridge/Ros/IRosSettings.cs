using System;

namespace Assets.Scripts.Bridge.Ros
{
    public interface IRosSettings
    {
        public string IpAddress { get; set; }

        public event EventHandler<string> IpAddressChanged;

        public int Port { get; set; }

        public event EventHandler<int> PortChanged;
    }
}
