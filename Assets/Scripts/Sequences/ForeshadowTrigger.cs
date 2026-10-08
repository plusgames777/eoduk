using UnityEngine;
using Eoduk.Core;

namespace Eoduk.Sequences
{
    [RequireComponent(typeof(Collider))]
    public sealed class ForeshadowTrigger : MonoBehaviour
    {
        [SerializeField] private string dialogueId = "DLG_Ch1_HatClue";
        [SerializeField] private GameObject silhouette;
        private void Reset() { GetComponent<Collider>().isTrigger = true; }
        private void OnTriggerEnter(Collider other) { if (!other.CompareTag("Player")) return; if (silhouette != null) silhouette.SetActive(true); GameEvents.RequestDialogue(dialogueId); gameObject.SetActive(false); }
    }
}
