using Assets.Scripts.Sensors;
using Assets.Scripts.Sensors.Camera;
using System.Collections.Generic;
using UnityEngine;
using Camera = Assets.Scripts.Sensors.Camera.Camera;

namespace Assets.Scripts.StaticSimulation.Spawner
{
    public sealed class Spawner : ISpawner
    {
        private readonly SpawnerSettings _settings;

        private readonly Sensors.Sensors _sensors;

        private readonly List<GameObject> _objects = new();

        public Spawner(
            SpawnerSettings settings,
            Sensors.Sensors sensors)
        {
            _settings = settings;
            _sensors = sensors;
        }

        public void Dispose()
        {
            Clear();
        }

        public void Spawn()
        {
            ISensetiveArea area = GetSensetiveArea();
            Vector3 point = area.GetSensetivePoint();

            GameObject prefab = GetPrefab();
            GameObject staticObject = Object.Instantiate(prefab, point, GetRotation());

            _objects.Add(staticObject);
        }

        public void Clear()
        {
            foreach (GameObject staticObject in _objects)
            {
                Object.Destroy(staticObject);
            }

            _objects.Clear();
        }

        private ISensetiveArea GetSensetiveArea()
        {
            List<Camera> list = _sensors.CameraList;
            return list[Random.Range(0, list.Count)].SensetiveArea;
        }

        private GameObject GetPrefab()
        {
            GameObject[] objects = _settings.Objects;
            return objects[Random.Range(0, objects.Length)];
        }

        private Quaternion GetRotation()
        {
            return Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        }
    }
}