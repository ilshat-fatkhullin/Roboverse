using System;
using UnityEngine;

namespace Assets.Scripts.Sensors.Lidar
{
    public sealed class RaysToPointCloudConverter : IDisposable
    {
        public readonly GraphicsBuffer PointCloudBuffer;

        public readonly GraphicsBuffer RaysBuffer;

        private readonly Transform _transform;

        private readonly ComputeShader _shader;

        private readonly int _kernelIndex;

        public RaysToPointCloudConverter(
            Transform transform,
            ComputeShader shader,
            int resolution,
            Faces faces,
            Vector3[] rays,
            UnityEngine.Camera camera)
        {
            _transform = transform;
            _shader = UnityEngine.Object.Instantiate(shader);

            RaysBuffer = new(GraphicsBuffer.Target.Structured, rays.Length, 12);
            RaysBuffer.SetData(rays);

            PointCloudBuffer = new(GraphicsBuffer.Target.Structured, rays.Length, 16);         

            _kernelIndex = _shader.FindKernel("CSMain");

            _shader.SetInt("FaceResolution", resolution);
            faces.SetTextures(_shader, _kernelIndex);
            _shader.SetBuffer(_kernelIndex, "Rays", RaysBuffer);
            _shader.SetBuffer(_kernelIndex, "PointCloud", PointCloudBuffer);
            _shader.SetFloat("NearClipPlane", camera.nearClipPlane);
            _shader.SetFloat("FarClipPlane", camera.farClipPlane);
        }

        public void Dispose()
        {
            PointCloudBuffer?.Release();
            RaysBuffer?.Release();
        }

        public void Convert()
        {
            _shader.SetVector("Origin", _transform.position);
            _shader.Dispatch(_kernelIndex, PointCloudBuffer.count / 32, 1, 1);
        }
    }
}
