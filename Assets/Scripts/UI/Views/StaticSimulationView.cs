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
                Panel,
                prefabs);
            _spawnAreaView = new SpawnAreaView(
                staticSimulation.SpawnArea,
                staticSimulation.SpawnAreaStorage,
                Panel,
                prefabs);
        }

        public override void Dispose()
        {
            base.Dispose();

            _simulationRunnerView.Dispose();
            _spawnAreaView.Dispose();            
        }
    }
}