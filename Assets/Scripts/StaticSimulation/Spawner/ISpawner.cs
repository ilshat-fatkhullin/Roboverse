using System;

namespace Assets.Scripts.StaticSimulation.Spawner
{
    public interface ISpawner : IDisposable
    {
        public void Spawn();

        public void Clear();
    }
}