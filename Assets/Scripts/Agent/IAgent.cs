using Assets.Scripts.Sensors;
using UnityEngine;

namespace Assets.Scripts.Agent
{
    public interface IAgent
    {
        public Vector3 Position { get; set; }

        public Quaternion Rotation { get; set; }

        public ISensors Sensors { get; }
    }
}