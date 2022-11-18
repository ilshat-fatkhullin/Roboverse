using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StaticSimulation.SpawnArea
{
    public interface ISpawnArea
    {
        public bool IsVisible { get; set; }

        public IReadOnlyCollection<SpawnBox> Boxes { get; }

        public SpawnBox GetBox(Vector3 position);

        public void AddBox(SpawnBox box);

        public void RemoveBox(SpawnBox box);
    }
}