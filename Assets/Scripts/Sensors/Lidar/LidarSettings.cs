using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Assets.Scripts.Sensors.Lidar
{
    [Serializable]
    public sealed class LidarSettings
    {
        public RayTracingShader RaysToPointCloudConverterShader;

        public ComputeShader PointCloudToDepthTextureConverterShader;
    }
}
