using System;

namespace Assets.Scripts.UI
{
    public interface IRoboversePanel : IDisposable
    {
        public bool IsVisible { get; set; }

        public event EventHandler<bool> IsVisibleChanged;

        public IRoboversePanel AddPanel(IUserInterfacePrefabs prefabs, string title);

        public IRoboverseField<string> AddField(
            IUserInterfacePrefabs prefabs, 
            string name, 
            Func<string> get, 
            Action<string> set);

        public IRoboverseField<int> AddField(
            IUserInterfacePrefabs prefabs, 
            string name, 
            Func<int> get, 
            Action<int> set);

        public IRoboverseField<float> AddField(
            IUserInterfacePrefabs prefabs, 
            string name, 
            Func<float> get, 
            Action<float> set);

        public IRoboverseButton AddButton(IUserInterfacePrefabs prefabs, string title);
    }
}