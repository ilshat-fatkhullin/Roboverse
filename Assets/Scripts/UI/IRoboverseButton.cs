using System;

namespace Assets.Scripts.UI
{
    public interface IRoboverseButton : IDisposable
    {
        public string Title { get; set; }

        public bool IsInteractable { get; set; }

        public event EventHandler Clicked;
    }
}