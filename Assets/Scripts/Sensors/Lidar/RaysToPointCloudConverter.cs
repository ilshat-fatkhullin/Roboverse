using System;
using UnityEngine;

namespace Assets.Scripts.Sensors.Lidar
{
    public sealed class RaysToPointCloudConverter : IDisposable
    {
        public readonly GraphicsBuffer PointCloudBuffer;

        private readonly GraphicsBuffer _raysBuffer;

        private readonly ComputeShader _shader;

        private readonly int _kernelIndex;

        public RaysToPointCloudConverter(
            ComputeShader shader,
            Faces faces,
            Vector3[] rays,
            float maxDistance)
        {
            _shader = UnityEngine.Object.Instantiate(shader);

            _raysBuffer = new(GraphicsBuffer.Target.Structured, rays.Length, 12);
            _raysBuffer.SetData(rays);

            PointCloudBuffer = new(GraphicsBuffer.Target.Structured, rays.Length, 16);         

            _kernelIndex = _shader.FindKernel("CSMain");

            faces.SetFaceParametersToShader(_shader, _kernelIndex);
            _shader.SetBuffer(_kernelIndex, "Rays", _raysBuffer);
            _shader.SetBuffer(_kernelIndex, "PointCloud", PointCloudBuffer);
            _shader.SetFloat("MaxDistance", maxDistance);
        }

        public void Dispose()
        {
            PointCloudBuffer?.Release();
            _raysBuffer?.Release();
        }

        public GraphicsBuffer Convert()
        {
            _shader.Dispatch(_kernelIndex, PointCloudBuffer.count / 32, 1, 1);
            return PointCloudBuffer;
        }
    }
}
