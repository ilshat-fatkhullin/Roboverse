using Assets.Scripts.StaticSimulation.SimulationRunner;
using System;

namespace Assets.Scripts.UI.Views.StaticSimulation
{
    public sealed class SimulationRunnerView : View, IDisposable
    {
        private readonly ISimulationRunner _simulationRunner;

        private readonly IRoboverseButton _startButton;

        private readonly IRoboverseButton _stopButton;

        public SimulationRunnerView(
            ISimulationRunner simulationRunner,
            IRoboversePanel parent,
            IUserInterfacePrefabs prefabs) : base(parent, prefabs, "Simulation runner")
        {
            _simulationRunner = simulationRunner;

            CreateSettings(simulationRunner.Settings);

            _startButton = Panel.AddButton(prefabs, "Start");
            _stopButton = Panel.AddButton(prefabs, "Stop");

            _startButton.Clicked += StartButton_Clicked;
            _stopButton.Clicked += StopButton_Clicked;
        }

        public override void Dispose()
        {
            base.Dispose();

            _startButton.Clicked -= StartButton_Clicked;
            _startButton.Dispose();
        }

        private void StartButton_Clicked(object sender, EventArgs e)
        {
            _simulationRunner.Start();
        }

        private void StopButton_Clicked(object sender, EventArgs e)
        {
            _simulationRunner.Stop();
        }
    }
}