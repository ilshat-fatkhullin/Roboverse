using Assets.Scripts.StaticSimulation;
using System;

namespace Assets.Scripts.UI.Views
{
    public class StaticSimulationView : View, IDisposable
    {
        private readonly IStaticSimulation _staticSimulation;

        private readonly IRoboverseButton _saveButton;

        private readonly IRoboverseButton _loadButton;

        public StaticSimulationView(
            IStaticSimulation staticSimulation,
            IRoboversePanel parent, 
            IUserInterfacePrefabs prefabs) : base(parent, prefabs, "Static simulation")
        {
            _staticSimulation = staticSimulation;
            _panel.IsVisibleChanged += IsVisibleChanged;
            IsVisibleChanged(this, _panel.IsVisible);

            CreateSettings(staticSimulation.Settings);
            
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
            _staticSimulation.IsActive = isVisible;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            _staticSimulation.SpawnAreaStorage.Save();
        }

        private void LoadButton_Clicked(object sender, EventArgs e)
        {
            _staticSimulation.SpawnAreaStorage.Load();
        }
    }
}