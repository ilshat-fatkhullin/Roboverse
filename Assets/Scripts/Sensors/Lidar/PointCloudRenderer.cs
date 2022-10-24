using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.VFX;

namespace Assets.Scripts.Sensors.Lidar
{
    public sealed class PointCloudRenderer : IDisposable
    {
        public GraphicsBuffer PointCloudBuffer => _raysToPointCloudConverter.PointCloudBuffer;

        private readonly UnityEngine.Camera _camera;

        private readonly int _measurements;

        private readonly int _raysCount;

        private readonly float _verticalAngle;

        private readonly VisualEffect _effect;

        private readonly RaysToPointCloudConverter _raysToPointCloudConverter;

        public PointCloudRenderer(
            UnityEngine.Camera camera,
            int measurements,
            int raysCount,
            float verticalAngle,
            RayTracingShader shader,
            VisualEffect effect)
        {
            _camera = camera;
            _measurements = measurements;
            _raysCount = raysCount;
            _verticalAngle = verticalAngle;
            _effect = effect;

            Vector3[] rays = CreateRays();

            _raysToPointCloudConverter = new(_camera, shader, rays);
        }

        public (Vector4[], GraphicsBuffer buffer) Render()
        {
            GraphicsBuffer pointCloudBuffer = _raysToPointCloudConverter.Convert();
            ReinitEffect(pointCloudBuffer);

            Vector4[] pointCloud = new Vector4[pointCloudBuffer.count];
            pointCloudBuffer.GetData(pointCloud);

            return (pointCloud, pointCloudBuffer);
        }

        public void Dispose()
        {
            _raysToPointCloudConverter.Dispose();
        }

        private void ReinitEffect(GraphicsBuffer pointCloudBuffer)
        {
            _effect.Reinit();
            _effect.SetGraphicsBuffer("PositionsBuffer", pointCloudBuffer);
        }

        private Vector3[] CreateRays()
        {
            int length = _measurements * _raysCount;

            Vector3[] rays = new Vector3[length];

            for (int x = 0; x < _measurements; x++)
                for (int y = 0; y < _raysCount; y++)
                {
                    float eulerX = y * _verticalAngle / _raysCount - _verticalAngle / 2;
                    float eulerY = x * 360f / _measurements + 180f;

                    Quaternion rotation = Quaternion.Euler(
                        -eulerX,
                        eulerY,
                        0);

                    Vector3 ray = rotation * Vector3.forward;

                    rays[y * _measurements + x] = ray;
                }

            return rays;
        }
    }
}
