using UnityEngine;
using Eoduk.Core;

namespace Eoduk.Systems
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class ProximityAudio : MonoBehaviour
    {
        [SerializeField] private Transform listener;
        [SerializeField] private float maxDistance = 20f;
        private AudioSource source;
        private void Awake() { source = GetComponent<AudioSource>(); source.loop = true; source.playOnAwake = false; }
        private void Start() { if (listener == null && Camera.main != null) listener = Camera.main.transform; }
        private void Update()
        {
            if (listener == null || source.clip == null) return;
            float d = Vector3.Distance(listener.position, transform.position);
            source.volume = Mathf.Clamp01(1f - d / maxDistance) * (SaveSystem.Settings?.sfxVolume ?? 1f);
            source.pitch = Mathf.Lerp(.85f, 1.2f, Mathf.Clamp01(1f - d / maxDistance));
            if (!source.isPlaying) source.Play();
        }
    }
}
