using Assets.Scripts.Sensors.Camera;
using Assets.Scripts.Sensors.GroundTruth2D;
using Assets.Scripts.Sensors.GroundTruth3D;
using Assets.Scripts.Sensors.Lidar;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Sensors
{
    public class SensorsView : MonoBehaviour, ISensorsView
    {
        public GameObject GameObject => gameObject;

        public IEnumerable<ICameraView> CameraViews => _cameraViews;

        public IEnumerable<IGroundTruth2DView> GroundTruth2DViews => _groundTruth2DViews;

        public IEnumerable<IGroundTruth3DView> GroundTruth3DViews => _groundTruth3DViews;

        public IEnumerable<ILidarView> LidarViews => _lidarViews;

        [SerializeField]
        private CameraView[] _cameraViews;

        [SerializeField]
        private GroundTruth2DView[] _groundTruth2DViews;

        [SerializeField]
        private GroundTruth3DView[] _groundTruth3DViews;

        [SerializeField]
        private LidarView[] _lidarViews;
    }
}