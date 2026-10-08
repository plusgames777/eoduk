using UnityEngine;
using Eoduk.Core;
using Eoduk.Core.Config;

namespace Eoduk.Player
{
    public sealed class BeadLight : MonoBehaviour
    {
        [SerializeField] private BeadConfig config;
        [SerializeField] private Light bead;
        public bool IsOn { get; private set; } = true;
        private void Update()
        {
            if (InputRouter.Pressed("BeadLight")) SetOn(!IsOn);
            if (bead != null) bead.enabled = IsOn;
        }
        public void SetOn(bool value) { IsOn = value; if (bead != null) bead.enabled = value; }
    }
}
