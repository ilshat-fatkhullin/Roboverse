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
        public string IpAddress;

        public int Port;

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
