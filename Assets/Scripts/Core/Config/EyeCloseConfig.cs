using UnityEngine;

namespace Eoduk.Core.Config
{
    [CreateAssetMenu(menuName = "Eoduk/Config/Eye Close", fileName = "CFG_EyeClose")]
    public sealed class EyeCloseConfig : ScriptableObject
    {
        public float maxDuration = 4f, cooldown = 3f, lidAnimTime = .2f;
    }
}
