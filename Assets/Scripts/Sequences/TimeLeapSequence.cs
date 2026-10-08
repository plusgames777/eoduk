using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Eoduk.Core;

namespace Eoduk.Sequences
{
    public sealed class TimeLeapSequence : MonoBehaviour
    {
        [SerializeField] private string targetScene = "Ch1_Well";
        [SerializeField] private CanvasGroup fade;
        [SerializeField] private float duration = 2f;
        private bool playing;
        public void Play() { if (!playing) StartCoroutine(Run()); }
        private IEnumerator Run()
        {
            playing = true; if (fade != null) fade.gameObject.SetActive(true);
            float t = 0;
            while (t < duration) { t += Time.unscaledDeltaTime; if (fade != null) fade.alpha = Mathf.Clamp01(t / duration); yield return null; }
            SceneManager.LoadScene(targetScene);
        }
    }
}
