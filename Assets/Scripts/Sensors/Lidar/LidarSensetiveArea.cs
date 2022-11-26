using UnityEngine;

namespace Assets.Scripts.Sensors.Lidar
{
    public class LidarSensetiveArea : ISensetiveArea
    {
        private readonly Lidar _lidar;

        public LidarSensetiveArea(Lidar lidar)
        {
            _lidar = lidar;
        }

        public void Dispose()
        {
            
        }

        public Vector3 GetSensetivePoint()
        {
            throw new System.NotImplementedException();
        }

        public bool IsSensetive(Vector3 point)
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }
    }
}