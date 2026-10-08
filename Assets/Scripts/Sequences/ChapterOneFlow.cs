using UnityEngine;
using Eoduk.Core;
using Eoduk.Enemies;

namespace Eoduk.Sequences
{
    public sealed class ChapterOneFlow : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private EoduksiniAI eoduksini;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform returnPoint;
        [SerializeField] private Transform apparitionPoint;
        [SerializeField] private FinalTurnSequence finale;
        [SerializeField] private float spawnDistance = 17f;
        private bool chaseStarted, returning, finished;
        public void BeginChase()
        {
            if (chaseStarted || eoduksini == null || player == null) return;
            chaseStarted = true;
            GameEvents.RequestDialogue("DLG_Ch1_Eoduksini");
            Vector3 point = player.position - player.forward * spawnDistance;
            if (spawnPoint != null) point = spawnPoint.position;
            eoduksini.Activate(player, point);
        }
        public void BeginReturn()
        {
            if (returning || player == null) return;
            returning = true;
            if (eoduksini != null) eoduksini.Deactivate();
            if (returnPoint != null) player.position = returnPoint.position;
            if (finale != null) finale.Begin();
        }
        public void StartFinale()
        {
            if (finished) return;
            finished = true;
            if (finale != null) finale.Begin();
        }
        private void OnEnable() { GameEvents.PlayerDied += OnDeath; }
        private void OnDisable() { GameEvents.PlayerDied -= OnDeath; }
        private void OnDeath(DeathCause _)
        {
            if (eoduksini != null) eoduksini.Deactivate(); chaseStarted = false;
            foreach (var trigger in FindObjectsByType<ChapterOneTrigger>(FindObjectsSortMode.None)) trigger.ResetTrigger();
        }
    }
}
