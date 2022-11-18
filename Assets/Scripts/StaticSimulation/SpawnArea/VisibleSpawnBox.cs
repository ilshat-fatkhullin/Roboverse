using System;
using UnityEngine;

namespace Assets.Scripts.StaticSimulation.SpawnArea
{
    public class VisibleSpawnBox : SpawnBox, IDisposable
    {
        public readonly GameObject Object;

        public VisibleSpawnBox(
            SpawnBox box,
            float step,
            Transform container,
            GameObject prefab) : base(box)
        {
            Object = UnityEngine.Object.Instantiate(prefab, GetPosition(step), Quaternion.identity, container);
            Object.transform.localScale = Vector3.one * step;
        }

        public void SetStep(float step)
        {
            Transform transform = Object.transform;

            transform.localPosition = GetPosition(step);
            transform.localScale = Vector3.one * step;
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(Object);
        }
    }
}