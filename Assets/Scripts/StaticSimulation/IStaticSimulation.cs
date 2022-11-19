using Assets.Scripts.Settings;
using Assets.Scripts.StaticSimulation.SpawnArea;

namespace Assets.Scripts.StaticSimulation
{
    public interface IStaticSimulation
    {
        public ISettings Settings { get; }

        public bool IsActive { get; set; }

        public ISpawnAreaStorage SpawnAreaStorage { get; }
    }
}