using UnityEngine;
using Eoduk.Core;

namespace Eoduk.Sequences
{
    public sealed class DialogueOnStart : MonoBehaviour
    {
        [SerializeField] private string dialogueId;
        private void Start() { if (!string.IsNullOrEmpty(dialogueId)) GameEvents.RequestDialogue(dialogueId); }
    }
}
