using RosMessageTypes.Geometry;
using RosMessageTypes.Sensor;
using UnityEngine;

namespace Assets.Scripts.Sensors.Lidar
{
    public static class PointCloudMessageBuilder
    {
        private const string MessageName = "point_cloud";

        public static PointCloudMsg Build(uint seq, Vector4[] pointCloud)
        {
            Point32Msg[] points = new Point32Msg[pointCloud.Length];

            for (int i = 0; i < pointCloud.Length; i++)
            {
                Vector4 point = pointCloud[i];
                points[i] = new Point32Msg(point.x, point.y, point.z);
            }

            return new() 
            {
                header = new()
                {
                    frame_id = MessageName,
                    seq = seq,
                    stamp = TimeMsgBuilder.Build()
                },
                points = points
            };
        }
    }
}
