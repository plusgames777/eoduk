using UnityEngine;
using Eoduk.Core;
using Eoduk.Core.Config;

namespace Eoduk.Player
{
    public sealed class EyeCloseController : MonoBehaviour
    {
        [SerializeField] private EyeCloseConfig config;
        [SerializeField] private CanvasGroup eyelids;
        public bool EyesClosed { get; private set; }
        public float ClosedFor { get; private set; }
        private float cooldown;
        private bool wasClosed;
        private void Update()
        {
            cooldown = Mathf.Max(0, cooldown - Time.deltaTime);
            if (InputRouter.Pressed("EyeClose") && (EyesClosed || cooldown <= 0)) EyesClosed = !EyesClosed;
            if (EyesClosed) { ClosedFor += Time.deltaTime; if (ClosedFor >= (config ? config.maxDuration : 4f)) { EyesClosed = false; cooldown = config ? config.cooldown : 3f; } }
            else ClosedFor = 0;
            if (wasClosed && !EyesClosed) cooldown = config ? config.cooldown : 3f;
            wasClosed = EyesClosed;
            if (eyelids != null) eyelids.alpha = Mathf.MoveTowards(eyelids.alpha, EyesClosed ? 1f : 0f, Time.deltaTime * 8f);
        }
    }
}
