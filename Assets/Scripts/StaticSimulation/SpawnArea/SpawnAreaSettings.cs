using Assets.Scripts.Settings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StaticSimulation.SpawnArea
{
    [Serializable]
    [CreateAssetMenu(fileName = "SpawnAreaSettings", menuName = "Settings/StaticSimulation/SpawnAreaSettings", order = 1)]
    public class SpawnAreaSettings : ScriptableObject, ISettings
    {
        public Vector3 Origin
        {
            get => _origin;
            set
            {
                _origin = value;
                OriginChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<Vector3> OriginChanged;

        public float Step
        {
            get => _step;
            set
            {
                _step = value;
                StepChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<float> StepChanged;

        public GameObject SpawnBoxPrefab;

        [SerializeField]
        private Vector3 _origin;

        [SerializeField]
        private float _step;

        public IReadOnlyCollection<FieldInfo<float>> FloatFields => new List<FieldInfo<float>>()
        {
            new FieldInfo<float>("Origin X", () => Origin.x, (value) => Origin = new Vector3(value, Origin.y, Origin.z)),
            new FieldInfo<float>("Origin Y", () => Origin.y, (value) => Origin = new Vector3(Origin.x, value, Origin.z)),
            new FieldInfo<float>("Origin Z", () => Origin.z, (value) => Origin = new Vector3(Origin.x, Origin.y, value)),
            new FieldInfo<float>("Step", () => Step, (value) => Step = value)
        };

        public IReadOnlyCollection<FieldInfo<int>> IntFields => new List<FieldInfo<int>>();

        public IReadOnlyCollection<FieldInfo<string>> StringFields => new List<FieldInfo<string>>();
    }
}