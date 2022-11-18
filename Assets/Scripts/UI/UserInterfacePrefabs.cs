using UnityEngine;

namespace Assets.Scripts.UI
{
    [CreateAssetMenu(fileName = "UserInterfacePrefabs", menuName = "UI/UserInterfacePrefabs", order = 1)]
    public class UserInterfacePrefabs : ScriptableObject, IUserInterfacePrefabs
    {
        public GameObject Panel => _panel;

        public GameObject Button => _button;

        public GameObject FloatField => _floatField;

        public GameObject IntField => _intField;

        public GameObject StringField => _stringField;

        [SerializeField]
        private GameObject _panel;

        [SerializeField]
        private GameObject _button;

        [SerializeField]
        private GameObject _floatField;

        [SerializeField]
        private GameObject _intField;

        [SerializeField]
        private GameObject _stringField;
    }
}