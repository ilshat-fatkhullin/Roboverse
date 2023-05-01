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
    }
}