using UnityEngine;

namespace Assets.Scripts.Sensors.Camera
{
    public class CameraSensetiveArea : ISensetiveArea
    {
        private readonly Camera _camera;

        private readonly LayerMask _layerMask = LayerMask.GetMask("Default");

        public CameraSensetiveArea(Camera camera)
        {
            _camera = camera;
        }

        public Vector3 GetSensetivePoint()
        {
            UnityEngine.Camera camera = _camera.UnityCamera;

            Vector3 screenPosition = new(
                camera.pixelWidth / 2,
                camera.pixelHeight / 2);

            Ray ray = camera.ScreenPointToRay(screenPosition);
            float distance = _camera.MaxDistance / 2f;

            for (int i = 0; i < 100; i++)
            {
                screenPosition = new(
                    Random.Range(0, camera.pixelWidth),
                    Random.Range(0, camera.pixelHeight));
                ray = camera.ScreenPointToRay(screenPosition);

                if (Physics.Raycast(ray, out RaycastHit hit, _camera.MaxDistance, _layerMask) &&
                    Vector3.Angle(hit.normal, Vector3.up) < 30f)
                {
                    distance = hit.distance;
                    break;
                }
            }

            return ray.origin + ray.direction.normalized * distance;
        }
    }
}