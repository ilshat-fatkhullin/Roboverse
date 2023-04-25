using Assets.Scripts.Settings;
using System;

namespace Assets.Scripts.Bridge
{
    public interface IBridge : IDisposable
    {
        public string Name { get; }

        public ISettings Settings { get; }
    }
}
