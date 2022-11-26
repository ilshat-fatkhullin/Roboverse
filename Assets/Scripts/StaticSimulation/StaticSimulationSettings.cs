using Assets.Scripts.StaticSimulation.SimulationRunner;
using Assets.Scripts.StaticSimulation.SpawnArea;
using Assets.Scripts.StaticSimulation.Spawner;
using System;
using UnityEngine;

namespace Assets.Scripts.StaticSimulation
{
    [Serializable]
    [CreateAssetMenu(fileName = "StaticSimulationSettings", menuName = "Settings/StaticSimulationSettings", order = 1)]
    public sealed class StaticSimulationSettings : ScriptableObject
    {
        public SimulationRunnerSettings SimulationRunnerSettings => _simulationRunnerSettings;

        public SpawnAreaSettings SpawnAreaSettings => _spawnAreaSettings;

        public SpawnerSettings SpawnerSettings => _spawnerSettings;

        [SerializeField]
        private SimulationRunnerSettings _simulationRunnerSettings;

        [SerializeField]
        private SpawnAreaSettings _spawnAreaSettings;

        [SerializeField]
        private SpawnerSettings _spawnerSettings;
    }
}