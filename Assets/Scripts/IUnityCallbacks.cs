using System;

namespace Assets.Scripts
{
    public interface IUnityCallbacks
    {
        public event EventHandler FixedUpdateOccured;

        public event EventHandler UpdateOccured;
    }
}
