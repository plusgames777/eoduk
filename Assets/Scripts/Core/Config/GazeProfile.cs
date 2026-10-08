using UnityEngine;

namespace Eoduk.Core.Config
{
    [CreateAssetMenu(menuName = "Eoduk/Config/Gaze Profile", fileName = "GAZE_Ch1")]
    public sealed class GazeProfile : ScriptableObject
    {
        public float startScale = 1f, minScale = 1f, maxScale = 2f;
        public float growPerSec = .35f, shrinkPerSec = .15f;
        public float seenSpeedPerScale = 1.2f, unseenSpeed = 2f;
        public float catchDistance = 1.2f, spawnMinDistance = 15f;
        public float sphereRadius = .05f, maxDistance = 30f;
        public LayerMask layers = ~0;
        [Range(.1f, 1f)] public float fovFraction = .7f;
        public float checkInterval = .05f;
    }
}
