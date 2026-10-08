using UnityEngine;
using Eoduk.Core;

namespace Eoduk.Systems
{
    [RequireComponent(typeof(Collider))]
    public sealed class PacingBeat : MonoBehaviour
    {
        [SerializeField] private string dialogueId;
        [SerializeField] private bool oneShot = true;
        private bool fired;
        private void Reset() { GetComponent<Collider>().isTrigger = true; }
        private void OnTriggerEnter(Collider other)
        {
            if (fired || !other.CompareTag("Player")) return;
            fired = true; if (!string.IsNullOrEmpty(dialogueId)) GameEvents.RequestDialogue(dialogueId);
            if (oneShot) gameObject.SetActive(false);
        }
    }
}
