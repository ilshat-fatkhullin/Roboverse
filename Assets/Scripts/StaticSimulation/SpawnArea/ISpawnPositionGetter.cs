using UnityEngine;

namespace Assets.Scripts.StaticSimulation.SpawnArea
{
    public interface ISpawnPositionGetter
    {
        public Vector3 GetAgentSpawnPosition();
    }
}