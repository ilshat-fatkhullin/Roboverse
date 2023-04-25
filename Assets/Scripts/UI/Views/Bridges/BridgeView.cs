using Assets.Scripts.Bridge;
using System;

namespace Assets.Scripts.UI.Views.Bridges
{
    public sealed class BridgeView : View, IDisposable
    {
        public BridgeView(
            IBridge bridge,
            IRoboversePanel parent, 
            IUserInterfacePrefabs prefabs) : base(parent, prefabs, bridge.Name)
        {
            CreateSettings(bridge.Settings);
        }
    }
}
