using System;
using UnityEngine;

namespace Assets.Scripts.Sensors.Lidar
{
    public interface IPointCloudToDepthTextureConverter : IDisposable
    {
        public Texture2D Convert();
    }
}
