using System;

namespace Assets.Scripts
{
    public interface IInputCallbacks
    {
        public event EventHandler FireOccured;

        public event EventHandler AltFireOccured;
    }
}