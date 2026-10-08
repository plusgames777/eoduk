using UnityEngine;
using Eoduk.Core;

namespace Eoduk.UI
{
    public sealed class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        private void Update()
        {
            if (!InputRouter.Pressed("Pause")) return;
            bool open = GameManager.Instance != null && !GameManager.Instance.IsPaused;
            GameManager.Instance?.SetPaused(open);
            if (panel != null) panel.SetActive(open);
        }
        public void Resume() { GameManager.Instance?.SetPaused(false); if (panel != null) panel.SetActive(false); }
        public void Quit() { GameManager.Instance?.SetPaused(false); Application.Quit(); }
    }
}
