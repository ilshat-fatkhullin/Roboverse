using Assets.Scripts.Agent;
using Assets.Scripts.Sensors;
using Assets.Scripts.Settings;
using Assets.Scripts.StaticSimulation.SpawnArea;
using Assets.Scripts.StaticSimulation.Spawner;
using System;
using UnityEngine;

namespace Assets.Scripts.StaticSimulation.SimulationRunner
{
    public sealed class SimulationRunner : ISimulationRunner
    {
        public bool IsRunning { get; private set; }

        public ISettings Settings => _settings;

        public event EventHandler<bool> IsRunningChanged;

        private readonly SimulationRunnerSettings _settings;

        private readonly IAgent _agent;

        private readonly ISpawnPositionGetter _spawnPositionGetter;

        private readonly IUnityCallbacks _callbacks;

        private readonly ISpawner _spawner;

        private int _currentMeasurement;

        private float _nextMeasurementTime;

        public SimulationRunner(
            SimulationRunnerSettings settings,
            IAgent agent,
            ISpawnPositionGetter spawnPositionGetter,
            IUnityCallbacks callbacks,
            ISpawner spawner)
        {
            _settings = settings;
            _agent = agent;
            _spawnPositionGetter = spawnPositionGetter;
            _callbacks = callbacks;
            _spawner = spawner;

            _callbacks.UpdateOccured += OnUpdateOccured;
        }

        private void OnUpdateOccured(object sender, EventArgs e)
        {
            if (!IsRunning || 
                _currentMeasurement >= _settings.Measurements ||
                _nextMeasurementTime > Time.time)
            {
                return;
            }
            
            _agent.Sensors.Measure();
                        
            RespawnAgent();
            ResetMeasurementTime();
            _currentMeasurement++;
            
            _spawner.Clear();            
            _spawner.Spawn();
        }

        public void Start()
        {
            if (IsRunning)
            {
                return;
            }

            IsRunning = true;
            
            RespawnAgent();
            ResetMeasurementTime();
            _currentMeasurement = 0;
            _agent.Sensors.SetDatasetGeneration(true);

            _spawner.Clear();
            _spawner.Spawn();
        }

        public void Stop()
        {
            if (!IsRunning)
            {
                return;
            }

            IsRunning = false;
        }

        private void RespawnAgent()
        {
            Vector3 position = _spawnPositionGetter.GetAgentSpawnPosition();
            _agent.Position = position;
            _agent.Rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0f, 360f), 0);
        }

        private void ResetMeasurementTime()
        {
            _nextMeasurementTime = Time.time + _settings.Delay;
        }
    }
}