using UnityEngine;
using Eoduk.Core;
using Eoduk.Gaze;
using Eoduk.Core.Config;

namespace Eoduk.Enemies
{
    public sealed class EoduksiniAI : MonoBehaviour, IGazeTarget
    {
        [SerializeField] private Transform player;
        [SerializeField] private float growPerSecond = .35f, shrinkPerSecond = .15f, unseenSpeed = 2f, catchDistance = 1.2f;
        [SerializeField] private float minScale = 1f, maxScale = 2f;
        [SerializeField] private GazeProfile profile;
        [SerializeField] private bool active;
        private float gazeUntil;
        public bool IsActive => active;
        private void Awake() { if (GetComponent<Collider>() == null) { var capsule = gameObject.AddComponent<CapsuleCollider>(); capsule.height = 1.8f; capsule.radius = .42f; capsule.center = Vector3.up * .9f; } }
        private void Start() { if (player == null && Camera.main != null) player = Camera.main.transform; SetVisible(false); }
        private void Update()
        {
            if (!active || player == null || GameManager.Instance?.IsPaused == true) return;
            float currentScale = Mathf.Clamp(transform.localScale.x, minScale, maxScale);
            float speed = Time.time < gazeUntil ? (profile ? profile.seenSpeedPerScale : 1.2f) * currentScale : unseenSpeed;
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation((player.position - transform.position).normalized, Vector3.up);
            if (Vector3.Distance(transform.position, player.position) <= catchDistance) { active = false; GameEvents.RaisePlayerDied(DeathCause.Eoduksini); }
        }
        public void Activate(Transform target, Vector3 spawnPosition)
        {
            player = target; transform.position = spawnPosition; transform.localScale = Vector3.one * minScale; active = true; gazeUntil = 0; SetVisible(true); GameEvents.RaiseChaseChanged(true);
        }
        public void Deactivate() { active = false; SetVisible(false); GameEvents.RaiseChaseChanged(false); }
        public void OnGaze(float deltaTime) { gazeUntil = Time.time + .15f; float s = Mathf.Min(maxScale, transform.localScale.x + growPerSecond * deltaTime); transform.localScale = Vector3.one * s; }
        public void OnGazeLost(float deltaTime) { float s = Mathf.Max(minScale, transform.localScale.x - shrinkPerSecond * deltaTime); transform.localScale = Vector3.one * s; }
        private void SetVisible(bool value)
        {
            foreach (var r in GetComponentsInChildren<Renderer>(true)) r.enabled = value;
            var collider = GetComponent<Collider>(); if (collider != null) collider.enabled = value;
        }
    }
}
