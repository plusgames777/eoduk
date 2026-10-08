using UnityEngine;
using Eoduk.Core;
using Eoduk.Core.Config;

namespace Eoduk.Systems
{
    public sealed class InteractionSystem : MonoBehaviour
    {
        [SerializeField] private Camera viewCamera;
        [SerializeField] private InteractionConfig config;
        [SerializeField] private LayerMask mask = ~0;
        public string CurrentPrompt { get; private set; }
        public IInteractable CurrentTarget { get; private set; }
        private void Update()
        {
            CurrentTarget = null; CurrentPrompt = null;
            if (viewCamera == null || GameManager.Instance?.IsPaused == true) return;
            if (Physics.Raycast(viewCamera.transform.position, viewCamera.transform.forward, out var hit, config ? config.rayDistance : 2f, mask, QueryTriggerInteraction.Collide))
            {
                CurrentTarget = hit.collider.GetComponentInParent<IInteractable>();
                if (CurrentTarget != null && CurrentTarget.CanInteract(gameObject)) CurrentPrompt = CurrentTarget.Prompt;
            }
            if (CurrentTarget != null && CurrentPrompt != null && InputRouter.Pressed("Interact")) CurrentTarget.Interact(gameObject);
        }
    }
}
