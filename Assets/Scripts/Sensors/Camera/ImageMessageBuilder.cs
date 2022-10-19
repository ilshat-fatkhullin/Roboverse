using RosMessageTypes.Sensor;
using System;

namespace Assets.Scripts.Sensors.Camera
{
    public sealed class ImageMessageBuilder
    {
        private const string MessageName = "frame";

        private readonly UnityEngine.Camera _camera;

        public ImageMessageBuilder(UnityEngine.Camera camera)
        {
            _camera = camera;
        }

        public ImageMsg Build(uint seq, byte[] data) =>
            new()
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
}
