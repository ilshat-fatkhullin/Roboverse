using Assets.Scripts.StaticSimulation;
using System;

namespace Assets.Scripts.UI.Views
{
    public class StaticSimulationView : View, IDisposable
    {
        private readonly IStaticSimulation _staticSimulation;

        public StaticSimulationView(
            IStaticSimulation staticSimulation,
            IRoboversePanel parent, 
            IUserInterfacePrefabs prefabs) : base(parent, prefabs, "Static simulation")
        {
            _staticSimulation = staticSimulation;
            _panel.IsVisibleChanged += IsVisibleChanged;
            IsVisibleChanged(this, _panel.IsVisible);

            CreateSettings(staticSimulation.Settings);         
        }

        public void Dispose()
        {
            _panel.IsVisibleChanged -= IsVisibleChanged;
        }

        private void IsVisibleChanged(object sender, bool isVisible)
        {
            _staticSimulation.IsActive = isVisible;
        }
    }
}