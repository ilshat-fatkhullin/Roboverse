using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Assets.Scripts.Sensors.Lidar
{
    [CreateAssetMenu(fileName = "LidarSettings", menuName = "Settings/Sensors/Lidar", order = 1)]
    public sealed class LidarSettings : ScriptableObject
    {
        public RayTracingShader RaysToPointCloudConverterShader;

        public ComputeShader PointCloudToDepthTextureConverterShader;
    }
}
