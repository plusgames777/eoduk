using UnityEngine;
using Eoduk.Systems;

namespace Eoduk.Sequences
{
    public sealed class SequenceInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string prompt = "조사하기 [E]";
        [SerializeField] private TimeLeapSequence timeLeap;
        [SerializeField] private GumiGuide gumi;
        [SerializeField] private FinalTurnSequence finalTurn;
        [SerializeField] private bool disableAfterUse = true;
        public string Prompt => prompt;
        public bool CanInteract(GameObject actor) => enabled;
        public void Interact(GameObject actor)
        {
            if (timeLeap != null) timeLeap.Play();
            else if (gumi != null) gumi.Handover();
            else if (finalTurn != null) finalTurn.Begin();
            if (disableAfterUse) enabled = false;
        }
    }
}
