using UnityEngine;

namespace Eoduk.Player
{
    [RequireComponent(typeof(Camera))]
    public sealed class CamcorderNightVision : MonoBehaviour
    {
        private Camera cam;
        private void Awake() { cam = GetComponent<Camera>(); cam.backgroundColor = new Color(.04f,.13f,.055f); cam.clearFlags = CameraClearFlags.SolidColor; }
    }
}
