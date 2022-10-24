using UnityEngine.VFX;

namespace Assets.Scripts.Sensors.Lidar
{
    public interface ILidarView : ISensorView
    {
        public UnityEngine.Camera Camera { get; }

        public int Measurements { get; }

        public int RaysCount { get; }

        public float VerticalAngle { get; }

        public VisualEffect VisualEffect { get; }
    }
}