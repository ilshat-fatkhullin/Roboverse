using System;
using UnityEngine;

namespace Assets.Scripts.Sensors.Lidar
{
    public sealed class EquirectToPointCloudConverter : IDisposable
    {
        public readonly GraphicsBuffer PointCloudBuffer;

        private readonly Transform _transform;

        private readonly ComputeShader _shader;

        private readonly RenderTexture _texture;

        private readonly int _kernelIndex;

        public EquirectToPointCloudConverter(
            Transform transform,
            ComputeShader shader,
            RenderTexture texture,
            UnityEngine.Camera camera)
        {
            _transform = transform;
            _shader = UnityEngine.Object.Instantiate(shader);
            _texture = texture;

            PointCloudBuffer = new(GraphicsBuffer.Target.Structured, _texture.width * _texture.height, 16);

            _kernelIndex = _shader.FindKernel("CSMain");

            _shader.SetTexture(_kernelIndex, "EquirectTexture", _texture);
            _shader.SetBuffer(_kernelIndex, "PointCloud", PointCloudBuffer);            

            _shader.SetInt("Width", _texture.width);
            _shader.SetInt("Height", _texture.height);

            _shader.SetFloat("NearClipPlane", camera.nearClipPlane);
            _shader.SetFloat("FarClipPlane", camera.farClipPlane);
        }

        public void Dispose()
        {
            PointCloudBuffer?.Release();
        }

        public void Convert()
        {
            _shader.SetVector("Origin", _transform.position);
            _shader.Dispatch(_kernelIndex, _texture.width / 8, _texture.height / 8, 1);
        }
    }
}
