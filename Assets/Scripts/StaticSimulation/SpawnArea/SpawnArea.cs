using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StaticSimulation.SpawnArea
{
    public class SpawnArea : ISpawnArea, IDisposable
    {
        public Vector3 Origin 
        { 
            get => _settings.Origin; 
            set => _settings.Origin = value; 
        }

        public float Step
        {
            get => _settings.Step;
            set => _settings.Step = value;
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                _container.SetActive(value);
            }
        }

        public IReadOnlyCollection<SpawnBox> Boxes => _boxes;

        private readonly StaticSimulationSettings _settings;

        private readonly GameObject _spawnBoxPrefab;

        private readonly GameObject _container;

        private readonly HashSet<SpawnBox> _boxes = new(new SpawnBoxEquialityComparer());

        private readonly Dictionary<SpawnBox, VisibleSpawnBox> _visibleBoxes = new(new SpawnBoxEquialityComparer());

        private readonly SpawnBoxEquialityComparer _comparer = new();

        private readonly SpawnBox _rootBox = new(0, 0, 0);

        private bool _isVisible;

        public SpawnArea(StaticSimulationSettings settings)
        {
            _settings = settings;
            _spawnBoxPrefab = _settings.SpawnBoxPrefab;
            
            _container = new GameObject("SpawnArea");
            _container.transform.position = _settings.Origin;

            AddBox(new SpawnBox(0, 0, 0));

            _settings.OriginChanged += OriginChanged;
            _settings.StepChanged += StepChanged;
        }

        public void Dispose()
        {
            _settings.OriginChanged -= OriginChanged;
            _settings.StepChanged -= StepChanged;

            UnityEngine.Object.Destroy(_container);
        }

        public void AddBox(SpawnBox box)
        {
            if (_boxes.Contains(box))
            {
                return;
            }

            _boxes.Add(box);

            if (!IsBoxVisible(box))
            {
                return;
            }

            _visibleBoxes.Add(box, CreateVisibleBox(box));

            foreach (SpawnBox neighbour in box.GetNeighboringBoxes())
            {
                if (!_visibleBoxes.TryGetValue(neighbour, out VisibleSpawnBox visibleBox))
                {
                    continue;
                }

                if (IsBoxVisible(neighbour))
                {
                    continue;
                }

                visibleBox.Dispose();
                _visibleBoxes.Remove(neighbour);
            }
        }

        public void RemoveBox(SpawnBox box)
        {
            if (_comparer.Equals(box, _rootBox))
            {
                return;
            }

            if (!_boxes.Contains(box))
            {
                throw new ArgumentException($"Box {box} does not exist.");
            }

            _boxes.Remove(box);

            if (!_visibleBoxes.TryGetValue(box, out VisibleSpawnBox visibleBox))
            {
                return;
            }

            visibleBox.Dispose();
            _visibleBoxes.Remove(box);

            foreach (SpawnBox neighbour in box.GetNeighboringBoxes())
            {
                if (!_boxes.Contains(neighbour))
                {
                    continue;
                }

                if (_visibleBoxes.ContainsKey(neighbour))
                {
                    continue;
                }

                if (!IsBoxVisible(neighbour))
                {
                    continue;
                }

                _visibleBoxes.Add(neighbour, CreateVisibleBox(box));
            }
        }

        public SpawnBox GetBox(Vector3 worldPosition)
        {
            Vector3 localPosition = worldPosition - Origin;

            return new(
                Mathf.FloorToInt(localPosition.x / Step),
                Mathf.FloorToInt(localPosition.y / Step),
                Mathf.FloorToInt(localPosition.z / Step));
        }

        private VisibleSpawnBox CreateVisibleBox(SpawnBox box) =>
            new(box, Step, _container.transform, _spawnBoxPrefab);

        private void OriginChanged(object sender, Vector3 origin)
        {
            _container.transform.position = origin;
        }

        private void StepChanged(object sender, float step)
        {
            foreach (VisibleSpawnBox box in _visibleBoxes.Values)
            {
                box.SetStep(step);
            }
        }

        private bool IsBoxVisible(SpawnBox box)
        {
            foreach (SpawnBox neighbour in box.GetNeighboringBoxes())
            {
                if (!_boxes.Contains(neighbour))
                {
                    return true;
                }
            }

            return false;
        }
    }
}