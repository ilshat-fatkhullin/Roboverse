using Assets.Scripts.Bindings;
using Assets.Scripts.Sensors;

namespace Assets.Scripts.Agent
{
    public interface IAgent
    {
        public ITransform Transform { get; }

        public ISensors Sensors { get; }
    }
}