using System;

namespace Assets.Scripts.Bindings
{
    public interface IGameObject : IDisposable
    {
        public ITransform Transform { get; }
    }
}