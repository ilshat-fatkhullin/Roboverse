using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class InputCallbacks : MonoBehaviour, IInputCallbacks
    {
        public event EventHandler FireOccured;
        public event EventHandler AltFireOccured;

        private void OnFire()
        {
            FireOccured?.Invoke(this, EventArgs.Empty);
        }

        private void OnAltFire()
        {
            AltFireOccured?.Invoke(this, EventArgs.Empty);
        }
    }
}