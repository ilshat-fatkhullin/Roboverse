using Assets.Scripts.StaticSimulation;
using Assets.Scripts.UI.Views.StaticSimulation;
using System;

namespace Assets.Scripts.UI.Views
{
    public class StaticSimulationView : View, IDisposable
    {
        private readonly SimulationRunnerView _simulationRunnerView;

        private readonly SpawnAreaView _spawnAreaView;

        public StaticSimulationView(
            IStaticSimulation staticSimulation,
            IRoboversePanel parent, 
            IUserInterfacePrefabs prefabs) : base(parent, prefabs, "Static simulation")
        {
            _simulationRunnerView = new SimulationRunnerView(
                staticSimulation.SimulationRunner,
                _panel,
                prefabs);
            _spawnAreaView = new SpawnAreaView(
                staticSimulation.SpawnArea,
                staticSimulation.SpawnAreaStorage,
                _panel,
                prefabs);
        }

        public void Dispose()
        {
            _simulationRunnerView.Dispose();
            _spawnAreaView.Dispose();            
        }
    }
}