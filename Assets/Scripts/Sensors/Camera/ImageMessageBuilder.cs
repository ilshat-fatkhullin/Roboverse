using RosMessageTypes.Sensor;
using System;
using UnityEngine;

namespace Assets.Scripts.Sensors.Camera
{
    public sealed class ImageMessageBuilder : IDisposable
    {
        private const string MessageName = "frame";

        private readonly UnityEngine.Camera _camera;

        private readonly RenderTexture _renderTexture;

        public ImageMessageBuilder(UnityEngine.Camera camera)
        {
            _camera = camera;
            _renderTexture = new(camera.pixelWidth, camera.pixelHeight, 24);
            _renderTexture.Create();

            _camera.targetTexture = _renderTexture;
        }

        public ImageMsg Build(uint seq)
        {
            _camera.Render();

            Texture2D texture = RenderTextureToTexture2DConverter.Convert(_renderTexture);
            byte[] data = texture.EncodeToJPG();

            return new()
            {
                header = new()
                {
                    frame_id = MessageName,
                    seq = seq,
                    stamp = TimeMsgBuilder.Build()
                },
                width = Convert.ToUInt32(_camera.pixelWidth),
                height = Convert.ToUInt32(_camera.pixelHeight),
                encoding = "jpg",
                data = data,                
            };
        }

        public void Dispose()
        {
            _renderTexture.Release();
        }
    }
}
