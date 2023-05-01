using Assets.Scripts.Agent;
using System;

namespace Assets.Scripts.UI.Views
{
    public sealed class AgentView : View, IDisposable
    {
        public AgentView(
            IRoboversePanel parent, 
            IUserInterfacePrefabs prefabs,
            IAgent agent) : base(parent, prefabs, "Agent")
        {
            
        }
    }
}
