using Assets.Scripts.StaticSimulation.SpawnArea;
using System;

namespace Assets.Scripts.UI.Views.StaticSimulation
{
    public class SpawnAreaView : View, IDisposable
    {
        private readonly ISpawnArea _spawnArea;

        private readonly ISpawnAreaStorage _storage;

        private readonly IRoboverseButton _saveButton;

        private readonly IRoboverseButton _loadButton;

        public SpawnAreaView(
            ISpawnArea spawnArea,
            ISpawnAreaStorage storage,
            IRoboversePanel parent,
            IUserInterfacePrefabs prefabs) : base(parent, prefabs, "Spawn area")
        {
            _spawnArea = spawnArea;
            _storage = storage;
            _panel.IsVisibleChanged += IsVisibleChanged;
            IsVisibleChanged(this, _panel.IsVisible);

            CreateSettings(spawnArea.Settings);

            _saveButton = _panel.AddButton(_prefabs, "Save");
            _loadButton = _panel.AddButton(_prefabs, "Load");

            _saveButton.Clicked += SaveButton_Clicked;
            _loadButton.Clicked += LoadButton_Clicked;
        }

        public void Dispose()
        {
            _panel.IsVisibleChanged -= IsVisibleChanged;

            _saveButton.Dispose();
            _loadButton.Dispose();
        }

        private void IsVisibleChanged(object sender, bool isVisible)
        {
            _spawnArea.IsVisible = isVisible;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            _storage.Save();
        }

        private void LoadButton_Clicked(object sender, EventArgs e)
        {
            _storage.Load();
        }
    }
}