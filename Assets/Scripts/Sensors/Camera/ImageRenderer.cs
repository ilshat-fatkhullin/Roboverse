using System;
using UnityEngine;

namespace Assets.Scripts.Sensors.Camera
{
    public sealed class ImageRenderer : IDisposable
    {
        private readonly UnityEngine.Camera _camera;

        private readonly RenderTexture _renderTexture;

        public ImageRenderer(
            int width,
            int height,
            UnityEngine.Camera camera)
        {
            _camera = camera;
            _camera.aspect = (float)width / height;

            _renderTexture = new(width, height, 24);
            _renderTexture.Create();

            _camera.targetTexture = _renderTexture;
        }

        public byte[] Render()
        {
            _camera.Render();
            Texture2D texture = RenderTextureToTexture2DConverter.Convert(_renderTexture);
            return texture.EncodeToJPG();
        }

        public void Dispose()
        {
            _renderTexture.Release();
        }
    }
}
