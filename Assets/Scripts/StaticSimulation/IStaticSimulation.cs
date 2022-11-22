using Assets.Scripts.StaticSimulation.SimulationRunner;
using Assets.Scripts.StaticSimulation.SpawnArea;

namespace Assets.Scripts.StaticSimulation
{
    public interface IStaticSimulation
    {
        public ISpawnArea SpawnArea { get; }

        public ISpawnAreaStorage SpawnAreaStorage { get; }

        public ISimulationRunner SimulationRunner { get; }
    }
}