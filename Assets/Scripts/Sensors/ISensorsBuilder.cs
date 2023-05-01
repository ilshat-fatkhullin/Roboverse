namespace Assets.Scripts.Sensors
{
    public interface ISensorsBuilder
    {
        public ISensors Build(ISensorsView view, ISensorsSettings settings);
    }
}
