using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Eoduk.Core;

namespace Eoduk.Sequences
{
    public sealed class FinalTurnSequence : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private Transform apparition;
        [SerializeField] private CanvasGroup fade;
        [SerializeField] private float waitBeforeForcedTurn = 10f;
        private bool started;
        public void Begin() { if (!started) StartCoroutine(Run()); }
        private IEnumerator Run()
        {
            started = true; GameEvents.RequestDialogue("DLG_Ch1_ReturnWell");
            Vector3 startForward = player != null ? player.forward : Vector3.forward;
            float elapsed = 0;
            while (elapsed < waitBeforeForcedTurn && player != null)
            {
                if (Vector3.Angle(startForward, player.forward) >= 65f) break;
                elapsed += Time.deltaTime; yield return null;
            }
            if (player != null && apparition != null) { var toward = apparition.position - player.position; toward.y = 0; player.rotation = Quaternion.LookRotation(toward); }
            float t = 0; while (t < 1.5f) { t += Time.deltaTime; if (fade != null) fade.alpha = Mathf.Clamp01(t / 1.5f); yield return null; }
            GameEvents.CompleteChapter(1); SceneManager.LoadScene("Ch1_Complete");
        }
    }
}
