using Assets.Scripts.Sensors.Camera;
using Assets.Scripts.Sensors.Lidar;
using UnityEngine;

namespace Assets.Scripts.Sensors
{
    [CreateAssetMenu(fileName = "SensorsSettings", menuName = "Settings/Sensors", order = 1)]
    public sealed class SensorsSettings : ScriptableObject
    {
        public CameraSettings CameraSettings;

        public LidarSettings LidarSettings;
    }
}
