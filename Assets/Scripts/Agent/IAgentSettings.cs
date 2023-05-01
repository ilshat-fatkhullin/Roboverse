using Assets.Scripts.Sensors;

namespace Assets.Scripts.Agent
{
    public interface IAgentSettings
    {
        public ISensorsSettings SensorsSettings { get; }
    }
}
