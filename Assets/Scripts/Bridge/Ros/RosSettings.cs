using Assets.Scripts.Settings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Bridge.Ros
{
    [Serializable]
    [CreateAssetMenu(fileName = "RosSettings", menuName = "Settings/Bridge/Ros", order = 1)]
    public sealed class RosSettings : ScriptableObject, ISettings
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

        public IReadOnlyCollection<FieldInfo<float>> FloatFields => new List<FieldInfo<float>>();

        public IReadOnlyCollection<FieldInfo<int>> IntFields => new List<FieldInfo<int>>()
        {
            new FieldInfo<int>("Port", () => Port, (value) => Port = value)
        };

        public IReadOnlyCollection<FieldInfo<string>> StringFields => new List<FieldInfo<string>>()
        {
            new FieldInfo<string>("IP address", () => IpAddress, (value) => IpAddress = value)
        };
    }
}
