using UnityEngine;
using UnityEngine.UI;
using Eoduk.Systems;

namespace Eoduk.UI
{
    public sealed class InteractionPromptUI : MonoBehaviour
    {
        [SerializeField] private InteractionSystem interaction;
        [SerializeField] private Text label;
        private void Update()
        {
            if (label == null || interaction == null) return;
            label.text = string.IsNullOrEmpty(interaction.CurrentPrompt) ? "" : "[E] " + interaction.CurrentPrompt;
        }
    }
}
