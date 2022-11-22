using Assets.Scripts.Agent;
using Assets.Scripts.Bridge;
using Assets.Scripts.Bridge.Ros;
using Assets.Scripts.StaticSimulation;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Views;
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
        private InstallerSettings _settings;

        [SerializeField]
        private InputCallbacks _inputCallbacks;

        [SerializeField]
        private RoboversePanel _roboversePanel;

        [SerializeField]
        private UserInterfacePrefabs _userInterfacePrefabs;

        private ServiceProvider _provider;        

        private void Awake()
        {
            ServiceCollection collection = new();

            collection.AddSingleton<IUnityCallbacks>(this);
            collection.AddSingleton<IInputCallbacks>(_inputCallbacks);
            collection.AddSingleton<IUserInterfacePrefabs>(_userInterfacePrefabs);
            collection.AddSingleton<IRoboversePanel>(_roboversePanel);
            collection.AddSingleton<IAgentView>(_agentView);
            collection.AddSingleton<IRoboversePanel>(_roboversePanel);
            collection.AddSingleton(_settings.AgentSettings);
            collection.AddSingleton(_settings.BridgeSettings.RosSettings);
            collection.AddSingleton<IAgent, Agent.Agent>();
            collection.AddSingleton(_settings.StaticSimulationSettings);
            collection.AddSingleton<IStaticSimulation, StaticSimulation.StaticSimulation>();
            collection.AddSingleton<StaticSimulationView>();
            collection.AddSingleton<IBridge, RosBridge>();
            collection.AddSingleton<BridgeView>();

            _provider = collection.BuildServiceProvider();
            _provider.GetService<IAgent>();
            _provider.GetService<StaticSimulationView>();
            _provider.GetService<BridgeView>();
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