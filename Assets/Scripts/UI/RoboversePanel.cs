using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class RoboversePanel : MonoBehaviour, IRoboversePanel
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
        private RectTransform _fieldsContainer;

        [SerializeField]
        private RectTransform _panelsContainer;

        [SerializeField]
        private RectTransform _tabsContainer;

        public IRoboversePanel AddPanel(IUserInterfacePrefabs prefabs, string title)
        {
            GameObject buttonObject = Instantiate(prefabs.Button, _tabsContainer);
            GameObject panelObject = Instantiate(prefabs.Panel, _panelsContainer);
            panelObject.name = title;

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

        public IRoboverseField<string> AddField(
            IUserInterfacePrefabs prefabs, 
            string name, 
            Func<string> get,
            Action<string> set)
            => CreateField(prefabs.StringField, name, get, set);

        public IRoboverseField<int> AddField(
            IUserInterfacePrefabs prefabs, 
            string name, 
            Func<int> get, 
            Action<int> set) => 
            CreateField(prefabs.IntField, name, get, set);

        public IRoboverseField<float> AddField(
            IUserInterfacePrefabs prefabs, 
            string name, 
            Func<float> get, 
            Action<float> set) => 
            CreateField(prefabs.FloatField, name, get, set);

        public IRoboverseButton AddButton(IUserInterfacePrefabs prefabs, string title)
        {
            GameObject buttonObject = Instantiate(prefabs.Button, _fieldsContainer);            
            
            RoboverseButton button = buttonObject.GetComponent<RoboverseButton>();
            _buttons.Add(button);

            button.Title = title;

            return button;
        }

        private IRoboverseField<T> CreateField<T>(
            GameObject prefab, 
            string name,
            Func<T> get,
            Action<T> set)
        {
            GameObject fieldObject = Instantiate(prefab, _fieldsContainer);
            IRoboverseField<T> field = fieldObject.GetComponent<IRoboverseField<T>>();

            field.Title = name;
            field.Value = get();
            field.ValueChanged += (_, value) => set(value);            

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