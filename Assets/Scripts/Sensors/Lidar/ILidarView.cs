using UnityEngine.VFX;

namespace Assets.Scripts.Sensors.Lidar
{
    public interface ILidarView : ISensorView
    {
        public UnityEngine.Camera Camera { get; }

        public int Width { get; }

        public int Height { get; }

        public UnityEngine.Vector2 FieldOfView { get; }

        public VisualEffect VisualEffect { get; }
    }
}