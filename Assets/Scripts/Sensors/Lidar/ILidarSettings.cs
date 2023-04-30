using UnityEngine.Experimental.Rendering;
using UnityEngine;

namespace Assets.Scripts.Sensors.Lidar
{
    public interface ILidarSettings
    {
        public RayTracingShader RaysToPointCloudConverterShader { get; }

        public ComputeShader PointCloudToDepthTextureConverterShader { get; }
    }
}
