using System;
using UnityEngine;

namespace Assets.Scripts.StaticSimulation.Spawner
{
    [Serializable]
    [CreateAssetMenu(fileName = "SpawnerSettings", menuName = "Settings/StaticSimulation/SpawnerSettings", order = 1)]
    public sealed class SpawnerSettings : ScriptableObject
    {
        public GameObject[] Objects => _objects;

        [SerializeField]
        private GameObject[] _objects;
    }
}