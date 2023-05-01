using UnityEngine;

namespace Assets.Scripts.Bindings
{
    public interface ITransform
    {
        public Vector3 Position { get; set; }

        public Quaternion Rotation { get; set; }

        public Vector3 LocalPosition { get; set; }

        public Quaternion LocalRotation { get; set; }

        public ITransform Parent { get; set; }

        public Transform Transform { get; }
    }
}
