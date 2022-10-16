using Assets.Scripts.Bridge;
using System;

namespace Assets.Scripts.Agent
{
    public sealed class Agent : IDisposable
    {
        public Sensors.Sensors Sensors { get; }

        public Agent(
            IAgentView view,
            AgentSettings settings,
            IBridge bridge,
            IUnityCallbacks callbacks)
        {
            Sensors = new Sensors.Sensors(view.SensorsView, bridge, callbacks, settings.SensorsSettings);
        }

        public void Dispose()
        {
            Sensors.Dispose();
        }
    }
}
