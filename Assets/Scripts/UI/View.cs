using Assets.Scripts.Settings;

namespace Assets.Scripts.UI
{
    public abstract class View
    {
        protected readonly IUserInterfacePrefabs _prefabs;

        protected readonly IRoboversePanel _panel;

        public View(
            IRoboversePanel parent,
            IUserInterfacePrefabs prefabs,
            string title)
        {
            _prefabs = prefabs;
            _panel = parent.AddPanel(_prefabs, title);
        }

        protected void CreateSettings(ISettings settings)
        {
            foreach (FieldInfo<float> fieldInfo in settings.FloatFields)
            {
                _panel.AddField(_prefabs, fieldInfo);
            }

            foreach (FieldInfo<int> fieldInfo in settings.IntFields)
            {
                _panel.AddField(_prefabs, fieldInfo);
            }

            foreach (FieldInfo<string> fieldInfo in settings.StringFields)
            {
                _panel.AddField(_prefabs, fieldInfo);
            }
        }
    }
}