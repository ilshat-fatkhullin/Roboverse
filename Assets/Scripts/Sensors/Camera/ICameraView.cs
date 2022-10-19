namespace Assets.Scripts.Sensors.Camera
{
    public interface ICameraView : ISensorView
    {
        public int Width { get; }

        public int Height { get; }

        public UnityEngine.Camera Camera { get; }
    }
}
