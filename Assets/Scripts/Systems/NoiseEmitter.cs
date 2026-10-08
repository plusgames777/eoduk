using UnityEngine;
using Eoduk.Core;

namespace Eoduk.Systems
{
    public sealed class NoiseEmitter : MonoBehaviour
    {
        public void Emit(float radius) => GameEvents.RaiseNoise(transform.position, radius);
        private void OnEnable() => GameEvents.NoiseMade += OnNoise;
        private void OnDisable() => GameEvents.NoiseMade -= OnNoise;
        private void OnNoise(Vector3 position, float radius) { }
    }
}
