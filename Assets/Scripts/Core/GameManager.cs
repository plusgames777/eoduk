using UnityEngine;

namespace Eoduk.Core
{
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public int CurrentChapter { get; set; } = 1;
        public bool IsPaused { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SaveSystem.Load();
            if (GetComponent<AudioManager>() == null) gameObject.AddComponent<AudioManager>();
            if (GetComponent<PlaytimeRecorder>() == null) gameObject.AddComponent<PlaytimeRecorder>();
        }

        public void SetPaused(bool value)
        {
            IsPaused = value;
            Time.timeScale = value ? 0f : 1f;
        }

        private void OnDestroy()
        {
            if (Instance == this) { Instance = null; Time.timeScale = 1f; }
        }
    }

    public sealed class AudioManager : MonoBehaviour
    {
        private AudioSource source;
        private void Awake()
        {
            if (GetComponent<AudioSource>() == null) source = gameObject.AddComponent<AudioSource>();
            else source = GetComponent<AudioSource>();
            source.playOnAwake = false;
            source.spatialBlend = 0f;
        }

        public void PlayOneShot(AudioClip clip, float volume = 1f)
        {
            if (clip != null && source != null) source.PlayOneShot(clip, volume * (SaveSystem.Settings?.sfxVolume ?? 1f) * (SaveSystem.Settings?.masterVolume ?? 1f));
        }
    }
}
