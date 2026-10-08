using UnityEngine;
using Eoduk.Core;

namespace Eoduk.Systems
{
    public sealed class WindSystem : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float baseStrength = .2f;
        [SerializeField] private float gustInterval = 18f, gustDuration = 4f;
        public float Strength { get; private set; }
        private float timer, gust;
        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= gustInterval) { timer = 0; gust = gustDuration; GameEvents.RaiseWindGust(true); }
            if (gust > 0) { gust -= Time.deltaTime; Strength = Mathf.Lerp(baseStrength, .55f, gust / gustDuration); if (gust <= 0) GameEvents.RaiseWindGust(false); }
            else Strength = baseStrength;
        }
    }
    [RequireComponent(typeof(Collider))]
    public sealed class WindZoneVolume : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float strength = .3f;
        private void Reset() { GetComponent<Collider>().isTrigger = true; }
        private void OnTriggerEnter(Collider other) { if (other.CompareTag("Player")) GameEvents.RaiseWindGust(strength > .2f); }
        private void OnTriggerExit(Collider other) { if (other.CompareTag("Player")) GameEvents.RaiseWindGust(false); }
    }
}
