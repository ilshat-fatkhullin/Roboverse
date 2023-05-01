using Assets.Scripts.Agent;
using Assets.Scripts.Bridge.Kafka;
using Assets.Scripts.Bridge.Ros;
using Assets.Scripts.Sensors;
using Assets.Scripts.Sensors.Camera;
using Assets.Scripts.Sensors.Lidar;
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
        private Agent.AgentView _agentView;

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
            collection.AddSingleton<IAgentSettings>(_settings.AgentSettings);
            collection.AddSingleton(_settings.BridgeSettings.RosSettings);
            collection.AddSingleton(_settings.BridgeSettings.KafkaSettings);

            // Agent
            collection.AddSingleton<IAgent, Agent.Agent>();

            // Bridge
            collection.AddSingleton<IRosBridge, RosBridge>();
            collection.AddSingleton<IKafkaBridge, KafkaBridge>();
            collection.AddSingleton<BridgesView>();

            // Sensors
            collection.AddSingleton<ISensorsBuilder, SensorsBuilder>();
            collection.AddSingleton<ICameraBuilder, CameraBuilder>();
            collection.AddSingleton<ILidarBuilder, LidarBuilder>();
            collection.AddSingleton<IPointCloudRendererBuilder, PointCloudRendererBuilder>();
            collection.AddSingleton<IPointCloudToDepthTextureConverterBuilder, PointCloudToDepthTextureConverterBuilder>();

            _provider = collection.BuildServiceProvider();
            _provider.GetService<IAgent>();
            _provider.GetService<BridgesView>();
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