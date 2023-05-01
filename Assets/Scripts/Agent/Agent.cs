using Assets.Scripts.Bindings;
using Assets.Scripts.Common;
using Assets.Scripts.Sensors;
using System;

namespace Assets.Scripts.Agent
{
    public sealed class Agent : IAgent, IDisposable
    {
        public ITransform Transform => _view.GameObject.Transform;

        public ISensors Sensors { get; }

        private readonly IAgentView _view;

        public Agent(
            IAgentView view,
            IAgentSettings settings,
            ISensorsBuilder sensorsBuilder)
        {
            _view = Throws.IfNull(view, nameof(view));
            Throws.IfNull(settings, nameof(settings));
            Throws.IfNull(sensorsBuilder, nameof(sensorsBuilder));

            Sensors = sensorsBuilder.Build(view.SensorsView, settings.SensorsSettings);
        }

        public void Dispose()
        {
            Sensors.Dispose();
        }
    }
}
