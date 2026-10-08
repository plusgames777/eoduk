using UnityEngine;

namespace Eoduk.Core.Config
{
    [CreateAssetMenu(menuName = "Eoduk/Config/Bead", fileName = "CFG_Bead")]
    public sealed class BeadConfig : ScriptableObject
    {
        public float lightRange = 12f, lightAngle = 55f, pointRadius = 2f;
        [Range(0f, 1f)] public float offIntensity = .1f;
        public Vector2 viewportPos = new Vector2(.18f, .82f);
        public float warnFar = 20f, warnNear = 10f, flickerSlowHz = 2f, flickerFastHz = 6f;
        public float intensity = 2.5f;
    }
}
