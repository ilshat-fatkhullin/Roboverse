using System;
using UnityEngine;
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

        private readonly RenderTexture _cubemap;

        private readonly RenderTexture _panorama;

        private readonly RaysToPointCloudConverter _raysToPointCloudConverter;

        public PointCloudRenderer(
            UnityEngine.Camera camera,
            int resolution,
            int measurements,
            int raysCount,
            float verticalAngle,
            ComputeShader shader,
            VisualEffect effect)
        {
            _camera = camera;
            _measurements = measurements;
            _raysCount = raysCount;
            _verticalAngle = verticalAngle;
            _effect = effect;
            _cubemap = new RenderTexture(resolution, resolution, 24, RenderTextureFormat.RFloat)
            {
                dimension = UnityEngine.Rendering.TextureDimension.Cube
            };
            _panorama = new RenderTexture(resolution * 2, resolution, 24, RenderTextureFormat.RFloat);

            (Vector3[] rays, Vector2[] coordinates) = CreateRaysAndAngles();

            _raysToPointCloudConverter = new(shader, _panorama, rays, coordinates, _camera.farClipPlane);
        }

        public (Vector4[], GraphicsBuffer buffer) Render()
        {
            _camera.RenderToCubemap(_cubemap, 51, UnityEngine.Camera.MonoOrStereoscopicEye.Left);
            _cubemap.ConvertToEquirect(_panorama, UnityEngine.Camera.MonoOrStereoscopicEye.Mono);

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

        private (Vector3[] rays, Vector2[] angles) CreateRaysAndAngles()
        {
            int length = _measurements * _raysCount;

            Vector3[] rays = new Vector3[length];
            Vector2[] coordinates = new Vector2[length];

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

                    int index = y * _measurements + x;

                    eulerX += 90;
                    eulerX /= 180f;

                    rays[index] = ray;
                    coordinates[index] = new Vector2((float)x / _measurements, (float)eulerX);
                }

            return (rays, coordinates);
        }
    }
}
