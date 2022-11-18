using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.StaticSimulation.SpawnArea
{
    public class SpawnAreaBuilder : ISpawnAreaBuilder, IDisposable
    {
        private readonly IInputCallbacks _inputCallbacks;

        private readonly ISpawnArea _spawnArea;

        private readonly LayerMask _layerMask = LayerMask.GetMask("SpawnArea");

        public SpawnAreaBuilder(
            IInputCallbacks inputCallbacks,
            ISpawnArea spawnArea)
        {
            _inputCallbacks = inputCallbacks;
            _spawnArea = spawnArea;

            _inputCallbacks.FireOccured += FireOccured;
            _inputCallbacks.AltFireOccured += AltFireOccured;
        }

        public void Dispose()
        {
            _inputCallbacks.FireOccured -= FireOccured;
        }

        private void FireOccured(object sender, EventArgs e)
        {
            SpawnBox box = GetSelectedBox();
            
            if (box == null)
            {
                return;
            }

            _spawnArea.AddBox(box);
        }

        private void AltFireOccured(object sender, EventArgs e)
        {
            SpawnBox box = GetSelectedBox(true);

            if (box == null)
            {
                return;
            }

            _spawnArea.RemoveBox(box);
        }

        private SpawnBox GetSelectedBox(bool returnNeighbour = false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _layerMask))
            {
                return null;
            }

            Vector3 point = hit.point;
            Bounds bounds = hit.collider.bounds;

            SpawnBox box = _spawnArea.GetBox(bounds.center);

            if (returnNeighbour)
            {
                return box;
            }

            if (point.x <= bounds.min.x)
            {
                return new SpawnBox(box.X - 1, box.Y, box.Z);
            }
            else if (point.x >= bounds.max.x)
            {
                return new SpawnBox(box.X + 1, box.Y, box.Z);
            }
            else if (point.y <= bounds.min.y)
            {
                return new SpawnBox(box.X, box.Y - 1, box.Z);
            }
            else if (point.y >= bounds.max.y)
            {
                return new SpawnBox(box.X, box.Y + 1, box.Z);
            }
            else if (point.z <= bounds.min.z)
            {
                return new SpawnBox(box.X, box.Y, box.Z - 1);
            }
            else if (point.z >= bounds.max.z)
            {
                return new SpawnBox(box.X, box.Y, box.Z + 1);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Raycast result is incorrect");
            }
        }
    }
}