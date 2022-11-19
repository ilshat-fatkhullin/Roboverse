using Assets.Scripts.Settings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class RoboversePanel : MonoBehaviour, IRoboversePanel, IDisposable
    {
        public bool IsVisible
        {
            get => gameObject.activeInHierarchy;
            set
            {
                gameObject.SetActive(value);
                IsVisibleChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<bool> IsVisibleChanged;

        private readonly List<RoboversePanel> _panels = new();

        private readonly List<RoboverseButton> _buttons = new();

        [SerializeField]
        private RectTransform _content;

        [SerializeField]
        private RectTransform _tabs;

        public IRoboversePanel AddPanel(IUserInterfacePrefabs prefabs, string title)
        {
            GameObject buttonObject = Instantiate(prefabs.Button, _tabs);
            GameObject panelObject = Instantiate(prefabs.Panel, _content);

            RoboverseButton button = buttonObject.GetComponent<RoboverseButton>();

            RoboversePanel panel = panelObject.GetComponent<RoboversePanel>();

            _panels.Add(panel);
            _buttons.Add(button);

            button.Title = title;
            button.Clicked += (_, _) => SelectPanel(panel, button);

            SelectPanel(panel, button);

            return panel;
        }

        public void Dispose()
        {
            foreach (RoboversePanel panel in _panels)
            {
                panel.Dispose();
            }

            Destroy(gameObject);
        }

        public IRoboverseField<string> AddField(IUserInterfacePrefabs prefabs, FieldInfo<string> info) => CreateField(prefabs.StringField, info);

        public IRoboverseField<int> AddField(IUserInterfacePrefabs prefabs, FieldInfo<int> info) => CreateField(prefabs.IntField, info);

        public IRoboverseField<float> AddField(IUserInterfacePrefabs prefabs, FieldInfo<float> info) => CreateField(prefabs.FloatField, info);

        public IRoboverseButton AddButton(IUserInterfacePrefabs prefabs, string title)
        {
            GameObject buttonObject = Instantiate(prefabs.Button, _content);            
            
            RoboverseButton button = buttonObject.GetComponent<RoboverseButton>();
            _buttons.Add(button);

            button.Title = title;

            return button;
        }

        private IRoboverseField<T> CreateField<T>(GameObject prefab, FieldInfo<T> info)
        {
            GameObject fieldObject = Instantiate(prefab, _content);
            IRoboverseField<T> field = fieldObject.GetComponent<IRoboverseField<T>>();

            field.Title = info.Name;
            field.Value = info.Get();
            field.ValueChanged += (_, value) => info.Set(value);

            return field;
        }

        private void SelectPanel(IRoboversePanel panel, IRoboverseButton button)
        {
            HideAllPanels();
            panel.IsVisible = true;
            button.IsInteractable = false;
        }

        private void HideAllPanels()
        {
            foreach (IRoboversePanel panel in _panels)
            {
                panel.IsVisible = false;
            }

            foreach (IRoboverseButton button in _buttons)
            {
                button.IsInteractable = true;
            }
        }
    }
}