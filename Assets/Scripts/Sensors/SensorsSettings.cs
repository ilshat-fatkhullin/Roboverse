using Assets.Scripts.Sensors.Camera;
using Assets.Scripts.Sensors.Lidar;
using UnityEngine;

namespace Assets.Scripts.Sensors
{
    [CreateAssetMenu(fileName = "SensorsSettings", menuName = "Settings/Sensors", order = 1)]
    public sealed class SensorsSettings : ScriptableObject, ISensorsSettings
    {
        public ICameraSettings CameraSettings => _cameraSettings;

        public ILidarSettings LidarSettings => _lidarSettings;

        [SerializeField]
        private CameraSettings _cameraSettings;

        [SerializeField]
        private LidarSettings _lidarSettings;
    }
}
