using System;
using UnityEngine;
using UnityEngine.VFX;

namespace Assets.Scripts.Sensors.Lidar
{
    public sealed class PointCloudRenderer : IDisposable
    {
        public GraphicsBuffer PointCloudBuffer => _raysToPointCloudConverter.PointCloudBuffer;

        private readonly UnityEngine.Camera _camera;

        private readonly int _resolution;

        private readonly int _measurements;

        private readonly int _raysCount;

        private readonly float _verticalAngle;

        private readonly VisualEffect _effect;

        public readonly Faces _faces;

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
            _resolution = resolution;
            _measurements = measurements;
            _raysCount = raysCount;
            _verticalAngle = verticalAngle;
            _effect = effect;

            _faces = new(_resolution);

            Vector3[] rays = CreateRays();

            _raysToPointCloudConverter = new(shader, _faces, rays, _camera.farClipPlane);
        }

        public (Vector4[], GraphicsBuffer buffer) Render()
        {
            RenderFace(_faces.Right, Vector3.right, Vector3.up);
            RenderFace(_faces.Left, Vector3.left, Vector3.up);
            RenderFace(_faces.Top, Vector3.up, Vector3.forward);
            RenderFace(_faces.Bottom, Vector3.down, Vector3.forward);
            RenderFace(_faces.Forward, Vector3.forward, Vector3.up);
            RenderFace(_faces.Back, Vector3.back, Vector3.up);

            GraphicsBuffer pointCloudBuffer = _raysToPointCloudConverter.Convert();
            ReinitEffect(pointCloudBuffer);

            Vector4[] pointCloud = new Vector4[pointCloudBuffer.count];
            pointCloudBuffer.GetData(pointCloud);

            return (pointCloud, pointCloudBuffer);
        }

        public void Dispose()
        {
            _faces.Dispose();
            _raysToPointCloudConverter.Dispose();
        }

        private void ReinitEffect(GraphicsBuffer pointCloudBuffer)
        {
            _effect.Reinit();
            _effect.SetGraphicsBuffer("PositionsBuffer", pointCloudBuffer);
        }

        private void RenderFace(RenderTexture face, Vector3 forward, Vector3 upward)
        {
            _camera.targetTexture = face;
            _camera.transform.localRotation = Quaternion.LookRotation(forward, upward);
            _camera.Render();
            _camera.transform.localRotation = Quaternion.identity;
        }

        private Vector3[] CreateRays()
        {
            Vector3[] rays = new Vector3[_measurements * _raysCount];

            for (int x = 0; x < _measurements; x++)
                for (int y = 0; y < _raysCount; y++)
                {
                    Quaternion rotation = Quaternion.Euler(
                        y * _verticalAngle / _raysCount - _verticalAngle / 2,
                        x * 360f / _measurements,
                        0);

                    Vector3 ray = rotation * Vector3.forward;

                    rays[x * _raysCount + y] = ray;
                }

            return rays;
        }
    }
}
