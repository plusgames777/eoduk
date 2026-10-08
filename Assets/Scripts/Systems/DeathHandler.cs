using System.Collections;
using UnityEngine;
using Eoduk.Core;

namespace Eoduk.Systems
{
    public sealed class DeathHandler : MonoBehaviour
    {
        [SerializeField] private Transform player;
        private Vector3 lastPosition;
        private float lastYaw;
        private void OnEnable() { GameEvents.PlayerDied += OnDied; GameEvents.CheckpointReached += Capture; }
        private void OnDisable() { GameEvents.PlayerDied -= OnDied; GameEvents.CheckpointReached -= Capture; }
        private void Start() { if (player == null) return; lastPosition = SaveSystem.Data.checkpointPosition; lastYaw = SaveSystem.Data.checkpointYaw; if (lastPosition == Vector3.zero) { lastPosition = player.position; lastYaw = player.eulerAngles.y; } }
        private void Capture(string _) { if (player != null) { lastPosition = player.position; lastYaw = player.eulerAngles.y; } }
        private void OnDied(DeathCause _) { StartCoroutine(Respawn()); }
        private IEnumerator Respawn()
        {
            if (player == null) yield break;
            yield return new WaitForSecondsRealtime(1.5f); player.gameObject.SetActive(false); player.SetPositionAndRotation(lastPosition, Quaternion.Euler(0, lastYaw, 0));
            if (player.TryGetComponent<CharacterController>(out var cc)) cc.enabled = true;
            player.gameObject.SetActive(true); Time.timeScale = 1; GameManager.Instance?.SetPaused(false);
        }
    }
}
