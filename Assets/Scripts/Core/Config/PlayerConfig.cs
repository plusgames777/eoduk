using UnityEngine;

namespace Eoduk.Core.Config
{
    [CreateAssetMenu(menuName = "Eoduk/Config/Player", fileName = "CFG_Player")]
    public sealed class PlayerConfig : ScriptableObject
    {
        public float walkSpeed = 3f, runSpeed = 5f, crouchSpeed = 1.5f;
        public float cameraHeight = 1.6f, crouchCameraHeight = 1f;
        public float colliderHeight = 1.8f, crouchColliderHeight = 1.2f;
        public float crouchTransition = .3f, runForwardAngle = 45f;
        public float leanOffset = .35f, leanAngle = 10f;
        public bool headBob = true;
    }
}
