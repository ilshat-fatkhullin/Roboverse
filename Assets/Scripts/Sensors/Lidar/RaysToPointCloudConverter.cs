using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Assets.Scripts.Sensors.Lidar
{
    public sealed class RaysToPointCloudConverter : IDisposable
    {
        public readonly GraphicsBuffer PointCloudBuffer;

        private readonly UnityEngine.Camera _camera;

        private readonly GraphicsBuffer _raysBuffer;        

        private readonly RayTracingShader _shader;

        private RayTracingAccelerationStructure _accelerationStructure;

        public RaysToPointCloudConverter(
            UnityEngine.Camera camera,
            RayTracingShader shader,
            Vector3[] rays)
        {
            _camera = camera;
            _shader = UnityEngine.Object.Instantiate(shader);

            _raysBuffer = new(GraphicsBuffer.Target.Structured, rays.Length, 12);
            _raysBuffer.SetData(rays);

            PointCloudBuffer = new(GraphicsBuffer.Target.Structured, rays.Length, 16);

            InitializeAccelerationStructure();

            _shader.SetAccelerationStructure("SceneAccelerationStructure", _accelerationStructure);
            _shader.SetBuffer("Rays", _raysBuffer);
            _shader.SetFloat("MaxDistance", _camera.farClipPlane);
            _shader.SetBuffer("PointCloud", PointCloudBuffer);
            _shader.SetShaderPass("PointCloud");
        }

        public void Dispose()
        {
            PointCloudBuffer.Release();
            _raysBuffer.Release();
            _accelerationStructure.Dispose();
        }

        public GraphicsBuffer Convert()
        {
            _shader.Dispatch("GenerateLidarRays", PointCloudBuffer.count, 1, 1, _camera);
            return PointCloudBuffer;
        }

        private void InitializeAccelerationStructure()
        {
            RayTracingAccelerationStructure.RASSettings settings = new RayTracingAccelerationStructure.RASSettings
            {
                layerMask = LayerMask.GetMask("Default"),
                managementMode = RayTracingAccelerationStructure.ManagementMode.Automatic,
                rayTracingModeMask = RayTracingAccelerationStructure.RayTracingModeMask.Everything
            };

            _accelerationStructure = new RayTracingAccelerationStructure(settings);

            Renderer[] renderers = UnityEngine.Object.FindObjectsOfType<Renderer>();
            foreach (Renderer r in renderers)
                _accelerationStructure.AddInstance(r);

            _accelerationStructure.Build();
        }
    }
}
