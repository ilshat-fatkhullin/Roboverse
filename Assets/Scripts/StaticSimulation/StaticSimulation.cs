using Assets.Scripts.Agent;
using Assets.Scripts.StaticSimulation.SimulationRunner;
using Assets.Scripts.StaticSimulation.SpawnArea;
using Assets.Scripts.StaticSimulation.Spawner;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Assets.Scripts.StaticSimulation
{
    public class StaticSimulation : IStaticSimulation, IDisposable
    {
        public ISpawnArea SpawnArea { get; }

        public ISpawnAreaStorage SpawnAreaStorage { get; }

        public ISimulationRunner SimulationRunner { get; }        

        private readonly ServiceProvider _provider;

        public StaticSimulation(
            StaticSimulationSettings settings,
            IInputCallbacks inputCallbacks,
            IUnityCallbacks callbacks,
            IAgent agent)
        {
            ServiceCollection collection = new();
            
            collection.AddSingleton<ISpawnArea>(s => new SpawnArea.SpawnArea(settings.SpawnAreaSettings));
            collection.AddSingleton<ISpawnAreaStorage>(s => new SpawnAreaStorage(s.GetService<ISpawnArea>()));
            collection.AddSingleton<ISpawnAreaBuilder>(s => new SpawnAreaBuilder(inputCallbacks, s.GetService<ISpawnArea>()));
            collection.AddSingleton<ISpawnPositionGetter>(s => new SpawnPositionGetter(s.GetService<ISpawnArea>()));
            collection.AddSingleton<ISimulationRunner>(s => new SimulationRunner.SimulationRunner(
                settings.SimulationRunnerSettings,
                agent,
                s.GetService<ISpawnPositionGetter>(),
                callbacks,
                s.GetService<ISpawner>()));
            collection.AddSingleton<ISpawner>(s => new Spawner.Spawner(settings.SpawnerSettings, agent.Sensors));

            _provider = collection.BuildServiceProvider();
            SpawnArea = _provider.GetRequiredService<ISpawnArea>();
            SpawnAreaStorage = _provider.GetRequiredService<ISpawnAreaStorage>();
            SimulationRunner = _provider.GetRequiredService<ISimulationRunner>();
            _provider.GetRequiredService<ISpawnAreaBuilder>();
        }

        public void Dispose()
        {
            _provider.Dispose();
        }
    }
}