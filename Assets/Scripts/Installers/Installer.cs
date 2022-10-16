using Assets.Scripts.Agent;
using Assets.Scripts.Bridge;
using Assets.Scripts.Bridge.Ros;
using Microsoft.Extensions.DependencyInjection;
using System;
using UnityEngine;

namespace Assets.Scripts.Installers
{
    public sealed class Installer : MonoBehaviour, IUnityCallbacks
    {
        public event EventHandler FixedUpdateOccured;

        public event EventHandler UpdateOccured;

        [SerializeField]
        private AgentView _agentView;

        [SerializeField]
        private AgentSettings _agentSettings;

        [SerializeField]
        private BridgeSettings _bridgeSettings;

        private ServiceProvider _provider;        

        private void Awake()
        {
            ServiceCollection collection = new();

            collection.AddSingleton<IUnityCallbacks>(this);
            collection.AddSingleton<IAgentView>(_agentView);
            collection.AddSingleton(_agentSettings);
            collection.AddSingleton(_bridgeSettings.RosSettings);
            collection.AddSingleton<Agent.Agent>();
            collection.AddSingleton<IBridge, RosBridge>();

            _provider = collection.BuildServiceProvider();
            _provider.GetService<Agent.Agent>();
        }

        private void OnDestroy()
        {
            _provider.Dispose();
        }

        private void Update()
        {
            UpdateOccured?.Invoke(this, EventArgs.Empty);
        }

        private void FixedUpdate()
        {
            FixedUpdateOccured?.Invoke(this, EventArgs.Empty);
        }
    }
}