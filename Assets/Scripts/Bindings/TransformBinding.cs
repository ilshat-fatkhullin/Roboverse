using UnityEngine;

namespace Assets.Scripts.Bindings
{
    public sealed class TransformBinding : ITransform
    {
        public Vector3 Position 
        {
            get => Transform.position;
            set => Transform.position = value; 
        }

        public Quaternion Rotation 
        { 
            get => Transform.rotation; 
            set => Transform.rotation = value; 
        }

        public Vector3 LocalPosition 
        { 
            get => Transform.localPosition; 
            set => Transform.localPosition = value; 
        }

        public Quaternion LocalRotation 
        { 
            get => Transform.localRotation; 
            set => Transform.localRotation = value;
        }

        public ITransform Parent 
        { 
            get => new TransformBinding(Transform.parent); 
            set => Transform.parent = value.Transform; 
        }

        public Transform Transform { get; }

        public TransformBinding(Transform transform)
        {
            Transform = transform;
        }
    }
}
