using UnityEngine;
using Eoduk.Core;

namespace Eoduk.Systems
{
    [RequireComponent(typeof(Collider))]
    public sealed class Checkpoint : MonoBehaviour
    {
        [SerializeField] private string checkpointId = "ch1_spawn";
        private void Reset() { GetComponent<Collider>().isTrigger = true; }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            SaveSystem.Data.checkpointId = checkpointId; SaveSystem.Data.checkpointPosition = other.transform.position; SaveSystem.Data.checkpointYaw = other.transform.eulerAngles.y;
            SaveSystem.Save(); GameEvents.RaiseCheckpointReached(checkpointId);
        }
    }
}
