using System;
using UnityEngine;

namespace Assets.Scripts.Bridge.Kafka
{
    [Serializable]
    [CreateAssetMenu(fileName = "KafkaSettings", menuName = "Settings/Bridge/Kafka", order = 1)]
    public sealed class KafkaSettings : ScriptableObject, IKafkaSettings
    {
        public string BootstrapServers 
        { 
            get => _bootstrapServers; 
            set
            {
                _bootstrapServers = value;
                BootstrapServersChanged?.Invoke(this, value);
            }
        }
        
        public event EventHandler<string> BootstrapServersChanged;

        [SerializeField]
        private string _bootstrapServers;
    }
}
