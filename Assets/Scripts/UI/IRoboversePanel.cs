using Assets.Scripts.Settings;
using System;

namespace Assets.Scripts.UI
{
    public interface IRoboversePanel
    {
        public bool IsVisible { get; set; }

        public event EventHandler<bool> IsVisibleChanged;

        public IRoboversePanel AddPanel(IUserInterfacePrefabs prefabs, string title);

        public IRoboverseField<string> AddField(IUserInterfacePrefabs prefabs, FieldInfo<string> info);

        public IRoboverseField<int> AddField(IUserInterfacePrefabs prefabs, FieldInfo<int> info);

        public IRoboverseField<float> AddField(IUserInterfacePrefabs prefabs, FieldInfo<float> info);
    }
}