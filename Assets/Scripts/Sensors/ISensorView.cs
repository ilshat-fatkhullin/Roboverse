namespace Assets.Scripts.Sensors
{
    public interface ISensorView : IView
    {
        public string Topic { get; }
    }
}
