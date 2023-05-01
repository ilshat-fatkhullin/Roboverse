using System;
using UnityEngine;

namespace Assets.Scripts.Bridge.Ros
{
    [Serializable]
    [CreateAssetMenu(fileName = "RosSettings", menuName = "Settings/Bridge/Ros", order = 1)]
    public sealed class RosSettings : ScriptableObject, IRosSettings
    {
        public string IpAddress
        {
            get => _ipAddress;
            set
            {
                _ipAddress = value;
                IpAddressChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<string> IpAddressChanged;

        public int Port
        {
            get => _port;
            set
            {
                _port = value;
                PortChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<int> PortChanged;

        [SerializeField]
        private string _ipAddress;

        [SerializeField]
        private int _port;
    }
}
