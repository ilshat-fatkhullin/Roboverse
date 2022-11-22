using Assets.Scripts.Settings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StaticSimulation.SimulationRunner
{
    [Serializable]
    [CreateAssetMenu(fileName = "SimulationRunnerSettings", menuName = "Settings/StaticSimulation/SimulationRunnerSettings", order = 1)]
    public class SimulationRunnerSettings : ScriptableObject, ISettings
    {
        public int Measurements => _measurements;

        public float Delay => _delay;

        [SerializeField]
        private int _measurements;

        [SerializeField]
        private float _delay;

        public IReadOnlyCollection<FieldInfo<float>> FloatFields => new List<FieldInfo<float>>()
        {
            new FieldInfo<float>("Delay", () => _delay, (value) => _delay = value)
        };

        public IReadOnlyCollection<FieldInfo<int>> IntFields => new List<FieldInfo<int>>()
        {
            new FieldInfo<int>("Measurements", () => _measurements, (value) => _measurements = value)
        };

        public IReadOnlyCollection<FieldInfo<string>> StringFields => new List<FieldInfo<string>>();
    }
}