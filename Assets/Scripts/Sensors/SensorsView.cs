using Assets.Scripts.Bindings;
using Assets.Scripts.Sensors.Camera;
using Assets.Scripts.Sensors.Lidar;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Sensors
{
    public class SensorsView : MonoBehaviour, ISensorsView
    {
        public IGameObject GameObject => new GameObjectBinding(gameObject);

        public IEnumerable<ICameraView> CameraViews => _cameraViews;

        public IEnumerable<ILidarView> LidarViews => _lidarViews;

        [SerializeField]
        private CameraView[] _cameraViews;

        [SerializeField]
        private LidarView[] _lidarViews;
    }
}