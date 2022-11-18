using System;

namespace Assets.Scripts.UI
{
    public interface IRoboverseField<T>
    {
        public string Title { get; set; }

        public T Value { get; set; }

        public event EventHandler<T> ValueChanged;
    }
}