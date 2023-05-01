using System;
using UnityEngine;

namespace Assets.Scripts.Sensors.Lidar
{
    public interface IPointCloudRenderer : IDisposable
    {
        public GraphicsBuffer PointCloudBuffer { get; }

        public (Vector4[], GraphicsBuffer buffer) Render();
    }
}
