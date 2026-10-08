using UnityEngine;
using UnityEngine.UI;
using Eoduk.Core;
using Eoduk.Core.Config;

namespace Eoduk.Player
{
    public sealed class CamcorderController : MonoBehaviour
    {
        [SerializeField] private Camera nightCamera;
        [SerializeField] private RawImage lcdScreen;
        [SerializeField] private CamcorderConfig config;
        private RenderTexture lcdTexture;
        public bool IsActive { get; private set; }
        private void Awake()
        {
            if (nightCamera != null)
            {
                if (config != null) nightCamera.fieldOfView = config.lcdFov;
                nightCamera.enabled = false;
                lcdTexture = new RenderTexture(640, 360, 16, RenderTextureFormat.ARGB32) { name = "RT_CamcorderLCD" };
                lcdTexture.Create(); nightCamera.targetTexture = lcdTexture;
            }
            if (lcdScreen != null) lcdScreen.enabled = false;
        }
        private void OnDestroy() { if (lcdTexture != null) { lcdTexture.Release(); Destroy(lcdTexture); } }
        private void Update()
        {
            if (InputRouter.Pressed("Camcorder")) SetActive(!IsActive);
            if (nightCamera != null) nightCamera.enabled = IsActive;
            if (lcdScreen != null) lcdScreen.enabled = IsActive;
        }
        public void SetActive(bool active) { IsActive = active; if (nightCamera != null) nightCamera.enabled = active; if (lcdScreen != null) lcdScreen.enabled = active; }
    }
}
