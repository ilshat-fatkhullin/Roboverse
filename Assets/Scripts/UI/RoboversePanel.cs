using Assets.Scripts.Settings;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

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

        private readonly List<Button> _buttons = new();

        [SerializeField]
        private RectTransform _content;

        [SerializeField]
        private RectTransform _tabs;

        public IRoboversePanel AddPanel(IUserInterfacePrefabs prefabs, string title)
        {
            GameObject buttonObject = Instantiate(prefabs.Button, _tabs);
            GameObject panelObject = Instantiate(prefabs.Panel, _content);

            Button button = buttonObject.GetComponent<Button>();
            TextMeshProUGUI textMesh = buttonObject.GetComponentInChildren<TextMeshProUGUI>();

            RoboversePanel panel = panelObject.GetComponent<RoboversePanel>();

            _panels.Add(panel);
            _buttons.Add(button);

            textMesh.text = title;
            button.onClick.AddListener(() => SelectPanel(panel, button));

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

        private IRoboverseField<T> CreateField<T>(GameObject prefab, FieldInfo<T> info)
        {
            GameObject fieldObject = Instantiate(prefab, _content);
            IRoboverseField<T> field = fieldObject.GetComponent<IRoboverseField<T>>();

            field.Title = info.Name;
            field.Value = info.Get();
            field.ValueChanged += (_, value) => info.Set(value);

            return field;
        }

        private void SelectPanel(IRoboversePanel panel, Button button)
        {
            HideAllPanels();
            panel.IsVisible = true;
            button.interactable = false;
        }

        private void HideAllPanels()
        {
            foreach (IRoboversePanel panel in _panels)
            {
                panel.IsVisible = false;
            }

            foreach (Button button in _buttons)
            {
                button.interactable = true;
            }
        }
    }
}