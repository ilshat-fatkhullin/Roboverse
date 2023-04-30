using Assets.Scripts.Agent;
using Assets.Scripts.Bridge;
using UnityEngine;

namespace Assets.Scripts.Installers
{
    [CreateAssetMenu(fileName = "InstallerSettings", menuName = "Settings/Installer", order = 1)]
    public sealed class InstallerSettings : ScriptableObject
    {
        public AgentSettings AgentSettings;

        public BridgeSettings BridgeSettings;
    }
}