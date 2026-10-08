using UnityEngine;
using Eoduk.Core;
using Eoduk.Core.Config;
using Eoduk.Player;

namespace Eoduk.Gaze
{
    public sealed class GazeSystem : MonoBehaviour
    {
        [SerializeField] private Camera viewCamera;
        [SerializeField] private CamcorderController camcorder;
        [SerializeField] private EyeCloseController eyes;
        [SerializeField] private GazeProfile profile;
        private IGazeTarget watchedTarget;
        private void Start()
        {
            foreach (var behaviour in FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None))
                if (behaviour is IGazeTarget gazeTarget) { watchedTarget = gazeTarget; break; }
        }
        private void Update()
        {
            IGazeTarget current = null;
            if (viewCamera != null && eyes?.EyesClosed != true && GameManager.Instance?.IsPaused != true)
            {
                float distance = profile ? profile.maxDistance : 30f;
                float radius = profile ? profile.sphereRadius : 0.05f;
                if (Physics.SphereCast(viewCamera.transform.position, radius, viewCamera.transform.forward, out var hit, distance, profile ? profile.layers : ~0, QueryTriggerInteraction.Ignore))
                {
                    var target = hit.collider.GetComponentInParent<MonoBehaviour>() as IGazeTarget;
                    if (target != null) current = target;
                }
            }
            if (watchedTarget != null)
            {
                if (current == watchedTarget) watchedTarget.OnGaze(Time.deltaTime);
                else watchedTarget.OnGazeLost(Time.deltaTime);
            }
        }
    }
}
