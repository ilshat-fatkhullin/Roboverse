using UnityEngine;

namespace Assets.Scripts.Bindings
{
    public sealed class GameObjectBinding : IGameObject
    {
        public ITransform Transform { get; }

        private readonly GameObject _gameObject;

        public GameObjectBinding(GameObject gameObject) 
        {
            _gameObject = gameObject;

            Transform = new TransformBinding(gameObject.transform);
        }

        public void Dispose()
        {
            Object.Destroy(_gameObject);
        }
    }
}
