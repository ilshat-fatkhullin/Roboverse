using Assets.Scripts.Bridge.Kafka;
using Assets.Scripts.Bridge.Ros;
using Assets.Scripts.UI.Views.Bridges;
using System;

namespace Assets.Scripts.UI.Views
{
    public sealed class BridgesView : View, IDisposable
    {
        private readonly BridgeView _rosBridgeView;

        private readonly BridgeView _kafkaBridgeView;

        public BridgesView(
            IRosBridge rosBridge,
            IKafkaBridge kafkaBridge,
            IRoboversePanel parent,
            IUserInterfacePrefabs prefabs) : base(parent, prefabs, "Bridges")
        {
            _rosBridgeView = new BridgeView(rosBridge, Panel, prefabs);
            _kafkaBridgeView = new BridgeView(kafkaBridge, Panel, prefabs);
        }

        public override void Dispose()
        {
            base.Dispose();

            _rosBridgeView.Dispose();
            _kafkaBridgeView.Dispose();
        }
    }
}