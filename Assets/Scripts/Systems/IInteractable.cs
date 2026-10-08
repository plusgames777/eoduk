using UnityEngine;

namespace Eoduk.Systems
{
    public interface IInteractable
    {
        string Prompt { get; }
        bool CanInteract(GameObject actor);
        void Interact(GameObject actor);
    }
}
