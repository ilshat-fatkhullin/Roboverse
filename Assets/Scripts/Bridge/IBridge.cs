using System;

namespace Assets.Scripts.Bridge
{
    public interface IBridge : IDisposable
    {
        public string Name { get; }
    }
}
