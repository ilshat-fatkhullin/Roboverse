using RosMessageTypes.Sensor;
using System;
using UnityEngine;
using UnityEngine.VFX;

namespace Assets.Scripts.Sensors.Lidar
{
    public sealed class PointCloudMessageBuilder : IDisposable
    {
        private const string MessageName = "point_cloud";

        private readonly UnityEngine.Camera _camera;

        private readonly int _width;

        private readonly int _height;

        private readonly VisualEffect _effect;

        private readonly RenderTexture _renderTexture;

        private readonly RenderTexture _equirectRenderTexture;

        private readonly EquirectToPointCloudConverter _equirectToPointCloudConverter;

        public PointCloudMessageBuilder(
            UnityEngine.Camera camera, 
            int width, 
            int height,
            ComputeShader shader,
            VisualEffect effect)
        {
            _camera = camera;

            _width = width;
            _height = height;

            _effect = effect;            

            _renderTexture = new(width, width, 24)
            {
                dimension = UnityEngine.Rendering.TextureDimension.Cube
            };
            _renderTexture.Create();

            _equirectRenderTexture = new(width * 2, width, 24);
            _equirectRenderTexture.Create();

            _equirectToPointCloudConverter = new(camera.transform, shader, _equirectRenderTexture, _camera);
            _effect.SetGraphicsBuffer("PositionsBuffer", _equirectToPointCloudConverter.PointCloudBuffer);
        }

        public PointCloudMsg Build(uint seq)
        {            
            _camera.RenderToCubemap(_renderTexture);
            _renderTexture.ConvertToEquirect(_equirectRenderTexture);

            _effect.Reinit();
            _equirectToPointCloudConverter.Convert();
            _effect.Play();

            return new() { };
        }

        public void Dispose()
        {
            _renderTexture.Release();
            _equirectRenderTexture.Release();
            _equirectToPointCloudConverter.Dispose();
        }
    }
}
