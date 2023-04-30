using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Assets.Scripts.Sensors.Lidar
{
    [CreateAssetMenu(fileName = "LidarSettings", menuName = "Settings/Sensors/Lidar", order = 1)]
    public sealed class LidarSettings : ScriptableObject, ILidarSettings
    {
        public RayTracingShader RaysToPointCloudConverterShader => _raysToPointCloudConverterShader;

        public ComputeShader PointCloudToDepthTextureConverterShader => _pointCloudToDepthTextureConverterShader;

        [SerializeField]
        private RayTracingShader _raysToPointCloudConverterShader;

        [SerializeField]
        private ComputeShader _pointCloudToDepthTextureConverterShader;
    }
}
