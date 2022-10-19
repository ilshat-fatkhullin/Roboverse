using System;
using UnityEngine;

namespace Assets.Scripts.Sensors.Lidar
{
    public sealed class PointCloudToDepthTextureConverter : IDisposable
    {
        private readonly ComputeShader _shader;

        private readonly RenderTexture _depthTexture;

        private readonly int _kernelIndex;

        public PointCloudToDepthTextureConverter(
            int measurements,
            int raysCount,
            float maxDistance,
            GraphicsBuffer pointCloudBuffer,
            ComputeShader shader)
        {
            _depthTexture = new RenderTexture(measurements, raysCount, 24, RenderTextureFormat.RFloat)
            {
                enableRandomWrite = true
            };
            _depthTexture.Create();

            _shader = UnityEngine.Object.Instantiate(shader);
            _kernelIndex = _shader.FindKernel("CSMain");
            
            _shader.SetBuffer(_kernelIndex, "PointCloud", pointCloudBuffer);
            _shader.SetTexture(_kernelIndex, "DepthTexture", _depthTexture);

            _shader.SetInt("RaysCount", raysCount);
            _shader.SetFloat("MaxDistance", maxDistance);
        }

        public Texture2D Convert()
        {
            _shader.Dispatch(_kernelIndex, _depthTexture.width / 8, _depthTexture.height / 8, 1);
            return RenderTextureToTexture2DConverter.Convert(_depthTexture, TextureFormat.RFloat);
        }

        public void Dispose()
        {
            _depthTexture.Release();
        }
    }
}
