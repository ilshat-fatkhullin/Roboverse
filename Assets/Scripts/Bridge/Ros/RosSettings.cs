using System;

namespace Assets.Scripts.Bridge.Ros
{
    [Serializable]
    public sealed class RosSettings
    {
        public string IpAddress;

        public int Port;
    }
}
