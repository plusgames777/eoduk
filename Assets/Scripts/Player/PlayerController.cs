using UnityEngine;
using Eoduk.Core;
using Eoduk.Core.Config;

namespace Eoduk.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig config;
        [SerializeField] private Transform cameraPivot;
        private CharacterController controller;
        private float pitch, baseCameraY, lean;
        private Vector3 velocity;
        private bool crouched;
        private float bobPhase;
        public bool IsCrouched => crouched;
        public Transform CameraPivot => cameraPivot;
        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            if (cameraPivot != null) baseCameraY = cameraPivot.localPosition.y;
        }
        private void Update()
        {
            if (GameManager.Instance?.IsPaused == true || cameraPivot == null) return;
            var look = InputRouter.Vector2("Look") * (SaveSystem.Settings?.mouseSensitivity ?? 1f) * .04f;
            transform.Rotate(0, look.x, 0); pitch = Mathf.Clamp(pitch - look.y, -85, 85);
            cameraPivot.localRotation = Quaternion.Euler(pitch, 0, lean);
            bool wantCrouch = InputRouter.Held("Crouch");
            float targetHeight = wantCrouch ? (config ? config.crouchColliderHeight : 1.2f) : (config ? config.colliderHeight : 1.8f);
            crouched = wantCrouch || (controller.height > targetHeight + .03f);
            if (!wantCrouch && Physics.SphereCast(transform.position + Vector3.up * controller.radius, controller.radius * .9f, Vector3.up, out _, targetHeight - controller.height, ~0, QueryTriggerInteraction.Ignore)) crouched = true;
            float speed = crouched ? (config ? config.crouchSpeed : 1.5f) : (InputRouter.Held("Run") && Mathf.Abs(InputRouter.Vector2("Move").y) > .7f && Mathf.Abs(InputRouter.Vector2("Move").x) < .7f ? (config ? config.runSpeed : 5f) : (config ? config.walkSpeed : 3f));
            controller.height = Mathf.MoveTowards(controller.height, crouched ? (config ? config.crouchColliderHeight : 1.2f) : (config ? config.colliderHeight : 1.8f), Time.deltaTime * (config ? config.colliderHeight : 1.8f) / (config ? config.crouchTransition : .3f));
            controller.center = Vector3.up * controller.height * .5f;
            float desiredY = crouched ? (config ? config.crouchCameraHeight : 1f) : (config ? config.cameraHeight : 1.6f);
            var p = cameraPivot.localPosition; p.y = Mathf.MoveTowards(p.y, desiredY, Time.deltaTime * 2f); cameraPivot.localPosition = p;
            Vector2 move = Vector2.ClampMagnitude(InputRouter.Vector2("Move"), 1f); Vector3 direction = transform.forward * move.y + transform.right * move.x;
            controller.Move((direction * speed + velocity) * Time.deltaTime);
            if (controller.isGrounded && velocity.y < 0) velocity.y = -1f; else velocity.y += Physics.gravity.y * Time.deltaTime;
            float targetLean = InputRouter.Held("LeanLeft") ? 1f : InputRouter.Held("LeanRight") ? -1f : 0f;
            lean = Mathf.Lerp(lean, targetLean * (config ? config.leanAngle : 10f), Time.deltaTime * 6f);
            if (direction.sqrMagnitude > .02f && controller.isGrounded) bobPhase += Time.deltaTime * (InputRouter.Held("Run") ? 13f : 9f);
            float bob = (SaveSystem.Settings?.headBob ?? true) && config != null && config.headBob && !crouched ? Mathf.Sin(bobPhase) * .025f : 0f;
            p = cameraPivot.localPosition; p.y = Mathf.MoveTowards(p.y, desiredY, Time.deltaTime * 2f) + bob; cameraPivot.localPosition = p;
            if (direction.sqrMagnitude > .02f && controller.isGrounded) GameEvents.RaiseNoise(transform.position, crouched ? 1f : InputRouter.Held("Run") ? 8f : 3f);
        }
    }

}
