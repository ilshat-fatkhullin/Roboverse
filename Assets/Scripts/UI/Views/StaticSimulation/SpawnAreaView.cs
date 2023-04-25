using Assets.Scripts.StaticSimulation.SpawnArea;
using System;

namespace Assets.Scripts.UI.Views.StaticSimulation
{
    public sealed class SpawnAreaView : View, IDisposable
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
            Panel.IsVisibleChanged += IsVisibleChanged;
            IsVisibleChanged(this, Panel.IsVisible);

            CreateSettings(spawnArea.Settings);

            _saveButton = Panel.AddButton(_prefabs, "Save");
            _loadButton = Panel.AddButton(_prefabs, "Load");

            _saveButton.Clicked += SaveButton_Clicked;
            _loadButton.Clicked += LoadButton_Clicked;
        }

        public override void Dispose()
        {
            base.Dispose();

            Panel.IsVisibleChanged -= IsVisibleChanged;

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