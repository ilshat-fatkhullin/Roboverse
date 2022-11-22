using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.StaticSimulation.SpawnArea
{
    public class SpawnPositionGetter : ISpawnPositionGetter
    {
        private readonly ISpawnArea _spawnArea;

        public SpawnPositionGetter(ISpawnArea spawnArea)
        {
            _spawnArea = spawnArea;
        }

        public Vector3 GetAgentSpawnPosition()
        {
            IReadOnlyCollection<SpawnBox> boxes = _spawnArea.Boxes;
            SpawnBox box = boxes.ElementAt(Random.Range(0, boxes.Count));

            return box.CreateSpawnPosition(_spawnArea.Step);
        }
    }
}