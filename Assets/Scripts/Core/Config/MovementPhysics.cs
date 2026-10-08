using UnityEngine;

namespace Eoduk.Core.Config
{
    [CreateAssetMenu(menuName = "Eoduk/Config/Movement Physics", fileName = "CFG_MovementPhysics")]
    public sealed class MovementPhysics : ScriptableObject
    {
        public float gravity = -9.81f;
        public float groundStick = -1f;
        public float headBobAmplitude = .025f, headBobFrequency = 1.7f;
        public float crouchClearance = 1.3f;
    }
}
