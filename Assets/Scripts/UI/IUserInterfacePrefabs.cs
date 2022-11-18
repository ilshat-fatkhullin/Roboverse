using UnityEngine;

namespace Assets.Scripts.UI
{
    public interface IUserInterfacePrefabs
    {
        public GameObject Panel { get; }

        public GameObject Button { get; }

        public GameObject FloatField { get; }

        public GameObject IntField { get; }

        public GameObject StringField { get; }
    }
}