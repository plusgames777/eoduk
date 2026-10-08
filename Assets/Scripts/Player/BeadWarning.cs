using UnityEngine;
using Eoduk.Core.Config;

namespace Eoduk.Player
{
    public sealed class BeadWarning : MonoBehaviour
    {
        [SerializeField] private BeadConfig config;
        [SerializeField] private Light bead;
        [SerializeField] private Transform threat;
        private float phase;
        private void Update()
        {
            if (bead == null || threat == null || config == null) return;
            float d = Vector3.Distance(transform.position, threat.position);
            float hz = d <= config.warnNear ? config.flickerFastHz : d <= config.warnFar ? config.flickerSlowHz : 0;
            phase += Time.deltaTime * hz * Mathf.PI * 2;
            bead.intensity = hz > 0 ? config.intensity * (.5f + .5f * Mathf.Sin(phase)) : config.intensity;
        }
    }
}
