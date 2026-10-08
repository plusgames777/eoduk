using UnityEngine;

namespace Eoduk.Core.Config
{
    [CreateAssetMenu(menuName = "Eoduk/Config/Interaction", fileName = "CFG_Interaction")]
    public sealed class InteractionConfig : ScriptableObject
    {
        public float rayDistance = 2f, holdTime = 2f;
    }
}
