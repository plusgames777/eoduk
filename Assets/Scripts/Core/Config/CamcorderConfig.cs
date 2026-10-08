using UnityEngine;

namespace Eoduk.Core.Config
{
    [CreateAssetMenu(menuName = "Eoduk/Config/Camcorder", fileName = "CFG_Camcorder")]
    public sealed class CamcorderConfig : ScriptableObject
    {
        public float batteryDuration = 180f;
        public bool batteryDrainEnabled;
        public float lcdFov = 50f;
        public Color nightVisionTint = new Color(.48f, 1f, .54f, 1f);
        [Range(0f, 1f)] public float noiseStrength = .25f;
    }
}
