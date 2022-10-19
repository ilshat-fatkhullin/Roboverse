using System;
using UnityEngine;

namespace Assets.Scripts.Sensors.Lidar
{
    public sealed class RaysToPointCloudConverter : IDisposable
    {
        public readonly GraphicsBuffer PointCloudBuffer;

        private readonly GraphicsBuffer _raysBuffer;

        private readonly GraphicsBuffer _coordinatesBuffer;

        private readonly ComputeShader _shader;

        private readonly int _kernelIndex;

        public RaysToPointCloudConverter(
            ComputeShader shader,
            RenderTexture panorama,
            Vector3[] rays,
            Vector2[] coordinates,
            float maxDistance)
        {
            _shader = UnityEngine.Object.Instantiate(shader);

            _raysBuffer = new(GraphicsBuffer.Target.Structured, rays.Length, 12);
            _raysBuffer.SetData(rays);

            _coordinatesBuffer = new(GraphicsBuffer.Target.Structured, rays.Length, 8);
            _coordinatesBuffer.SetData(coordinates);

            PointCloudBuffer = new(GraphicsBuffer.Target.Structured, rays.Length, 16);         

            _kernelIndex = _shader.FindKernel("CSMain");

            _shader.SetInt("Resolution", panorama.height);
            _shader.SetTexture(_kernelIndex, "Panorama", panorama);
            _shader.SetBuffer(_kernelIndex, "Rays", _raysBuffer);
            _shader.SetBuffer(_kernelIndex, "Angles", _coordinatesBuffer);
            _shader.SetBuffer(_kernelIndex, "PointCloud", PointCloudBuffer);
            _shader.SetFloat("MaxDistance", maxDistance);
        }

        public void Dispose()
        {
            PointCloudBuffer.Release();
            _raysBuffer.Release();
            _coordinatesBuffer.Release();
        }

        public GraphicsBuffer Convert()
        {
            _shader.Dispatch(_kernelIndex, PointCloudBuffer.count / 32, 1, 1);
            return PointCloudBuffer;
        }
    }
}
