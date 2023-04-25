using Assets.Scripts.Bridge;
using Assets.Scripts.Bridge.Ros;
using System;
using UnityEngine;

namespace Assets.Scripts.Agent
{
    public sealed class Agent : IAgent, IDisposable
    {
        public Vector3 Position
        {
            get => _view.GameObject.transform.position;
            set => _view.GameObject.transform.position = value;
        }

        public Quaternion Rotation
        {
            get => _view.GameObject.transform.rotation;
            set => _view.GameObject.transform.rotation = value;
        }

        public Sensors.Sensors Sensors { get; }

        private readonly IAgentView _view;

        public Agent(
            IAgentView view,
            AgentSettings settings,
            IRosBridge bridge)
        {
            _view = view;
            Sensors = new Sensors.Sensors(view.SensorsView, bridge, settings.SensorsSettings);
        }

        public void Dispose()
        {
            Sensors.Dispose();
        }
    }
}
