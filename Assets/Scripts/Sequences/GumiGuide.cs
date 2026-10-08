using UnityEngine;
using Eoduk.Core;

namespace Eoduk.Sequences
{
    public sealed class GumiGuide : MonoBehaviour
    {
        [SerializeField] private string dialogueId = "DLG_Ch1_GumiIntro";
        [SerializeField] private GameObject beadPickup;
        [SerializeField] private BeadHandoverSequence handover;
        private void Start() { if (!string.IsNullOrEmpty(dialogueId)) GameEvents.RequestDialogue(dialogueId); }
        public void Handover() { if (handover != null) handover.Play(); else if (beadPickup != null) beadPickup.SetActive(false); }
    }
}
