using Assets.Scripts.Sensors;

namespace Assets.Scripts.Agent
{
    public interface IAgentView : IView
    {
        public ISensorsView SensorsView { get; }
    }
}
