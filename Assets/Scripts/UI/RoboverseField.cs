using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public abstract class RoboverseField<T> : MonoBehaviour, IRoboverseField<T>
    {
        public T Value
        {
            get => ParseValue(_field.text);
            set => _field.text = value.ToString();
        }

        public string Title 
        { 
            get => _title.text; 
            set => _title.text = value; 
        }

        public event EventHandler<T> ValueChanged;

        [SerializeField]
        private TextMeshProUGUI _title;

        [SerializeField]
        private TMP_InputField _field;

        protected abstract T ParseValue(string value);

        private void Awake()
        {
            _field.onEndEdit.AddListener(OnValueChanged);
        }

        private void OnValueChanged(string content)
        {
            T value = ParseValue(content);
            _field.text = value.ToString();
            ValueChanged?.Invoke(this, value);
        }
    }
}