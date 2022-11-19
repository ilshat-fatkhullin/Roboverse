using Assets.Scripts.Settings;
using Assets.Scripts.StaticSimulation.SpawnArea;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Assets.Scripts.StaticSimulation
{
    public class StaticSimulation : IStaticSimulation, IDisposable
    {
        public ISettings Settings { get; }

        public bool IsActive 
        { 
            get => _isActive; 
            set
            {
                _isActive = value;
                _spawnArea.IsVisible = value;
            }
        }

        public ISpawnAreaStorage SpawnAreaStorage { get; }

        private bool _isActive;

        private readonly ServiceProvider _provider;

        private readonly ISpawnArea _spawnArea;

        public StaticSimulation(
            StaticSimulationSettings settings,
            IInputCallbacks inputCallbacks)
        {
            Settings = settings;

            ServiceCollection collection = new();
            
            collection.AddSingleton(settings);
            collection.AddSingleton(inputCallbacks);
            collection.AddSingleton<ISpawnArea, SpawnArea.SpawnArea>();
            collection.AddSingleton<ISpawnAreaStorage, SpawnAreaStorage>();
            collection.AddSingleton<ISpawnAreaBuilder, SpawnAreaBuilder>();

            _provider = collection.BuildServiceProvider();
            _spawnArea = _provider.GetRequiredService<ISpawnArea>();
            SpawnAreaStorage = _provider.GetRequiredService<ISpawnAreaStorage>();
            _provider.GetRequiredService<ISpawnAreaBuilder>();
        }

        public void Dispose()
        {
            _provider.Dispose();
        }
    }
}