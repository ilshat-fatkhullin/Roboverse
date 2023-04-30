namespace Assets.Scripts.Sensors.Camera
{
    public interface ICamera : ISensor
    {
        public int Width { get; }

        public int Height { get; }

        public float MaxDistance { get; }

        public UnityEngine.Camera UnityCamera { get; }
    }
}
