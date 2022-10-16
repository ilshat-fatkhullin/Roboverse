namespace Assets.Scripts.Sensors.Camera
{
    public interface ICameraView : ISensorView
    {
        public UnityEngine.Camera Camera { get; }
    }
}
