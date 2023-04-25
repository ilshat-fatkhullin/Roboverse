using Assets.Scripts.Settings;
using System;

namespace Assets.Scripts.UI
{
    public abstract class View : IDisposable
    {
        protected readonly IUserInterfacePrefabs _prefabs;

        protected readonly IRoboversePanel Panel;

        public View(
            IRoboversePanel parent,
            IUserInterfacePrefabs prefabs,
            string title)
        {
            _prefabs = prefabs;
            Panel = parent.AddPanel(_prefabs, title);
        }

        public virtual void Dispose()
        {
            Panel.Dispose();
        }

        protected void CreateSettings(ISettings settings)
        {
            foreach (FieldInfo<float> fieldInfo in settings.FloatFields)
            {
                Panel.AddField(_prefabs, fieldInfo);
            }

            foreach (FieldInfo<int> fieldInfo in settings.IntFields)
            {
                Panel.AddField(_prefabs, fieldInfo);
            }

            foreach (FieldInfo<string> fieldInfo in settings.StringFields)
            {
                Panel.AddField(_prefabs, fieldInfo);
            }
        }
    }
}