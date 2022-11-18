using Assets.Scripts.Settings;

namespace Assets.Scripts.StaticSimulation
{
    public interface IStaticSimulation
    {
        public ISettings Settings { get; }

        public bool IsActive { get; set; }
    }
}