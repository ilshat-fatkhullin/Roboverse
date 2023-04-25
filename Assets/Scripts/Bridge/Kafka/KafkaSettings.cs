using Assets.Scripts.Settings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Bridge.Kafka
{
    [Serializable]
    [CreateAssetMenu(fileName = "KafkaSettings", menuName = "Settings/Bridge/Kafka", order = 1)]
    public sealed class KafkaSettings : ScriptableObject, ISettings
    {
        public string BootstrapServers;

        public IReadOnlyCollection<FieldInfo<float>> FloatFields => new List<FieldInfo<float>>();

        public IReadOnlyCollection<FieldInfo<int>> IntFields => new List<FieldInfo<int>>();

        public IReadOnlyCollection<FieldInfo<string>> StringFields => new List<FieldInfo<string>>()
        {
            new FieldInfo<string>("Bootstrap servers", () => BootstrapServers, (value) => BootstrapServers = value)
        };
    }
}
