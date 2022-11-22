using Assets.Scripts.Settings;
using System;

namespace Assets.Scripts.StaticSimulation.SimulationRunner
{
    public interface ISimulationRunner
    {
        public bool IsRunning { get; }

        public event EventHandler<bool> IsRunningChanged;

        public ISettings Settings { get; }

        public void Start();

        public void Stop();
    }
}