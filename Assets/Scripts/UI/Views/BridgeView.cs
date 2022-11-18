using Assets.Scripts.Bridge;

namespace Assets.Scripts.UI.Views
{
    public sealed class BridgeView : View
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