using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class RoboverseButton : MonoBehaviour, IRoboverseButton
    {
        public string Title 
        { 
            get => _title.text; 
            set => _title.text = value; 
        }
        public bool IsInteractable 
        { 
            get => _button.interactable; 
            set => _button.interactable = value; 
        }

        public event EventHandler Clicked;

        [SerializeField]
        private TextMeshProUGUI _title;

        [SerializeField]
        private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}