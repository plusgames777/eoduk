using UnityEngine;

namespace Eoduk.Core
{
    public sealed class PlaytimeRecorder : MonoBehaviour
    {
        [SerializeField] private float saveInterval = 15f;
        private float elapsed;
        private void Update()
        {
            if (GameManager.Instance?.IsPaused == true) return;
            elapsed += Time.unscaledDeltaTime;
            SaveSystem.Data.playtimeSeconds += Time.unscaledDeltaTime;
            if (elapsed >= saveInterval) { elapsed = 0; SaveSystem.Save(); }
        }
        private void OnApplicationPause(bool paused) { if (paused) SaveSystem.Save(); }
        private void OnApplicationQuit() { SaveSystem.Save(); }
    }
}
