using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Eoduk.Core;

namespace Eoduk.UI
{
    public sealed class DialogueUI : MonoBehaviour
    {
        [SerializeField] private Text speakerText, bodyText;
        [SerializeField] private CanvasGroup group;
        private void OnEnable() => GameEvents.DialogueRequested += ShowById;
        private void OnDisable() => GameEvents.DialogueRequested -= ShowById;
        public void ShowById(string id)
        {
            var table = Resources.Load<DialogueTable>("Dialogue/" + id);
            if (table == null) { Debug.LogWarning("Dialogue table not found: " + id); return; }
            StopAllCoroutines(); StartCoroutine(Play(table));
        }
        private IEnumerator Play(DialogueTable table)
        {
            if (group != null) group.alpha = 1;
            foreach (var line in table.lines)
            {
                if (speakerText != null) speakerText.text = line.speaker;
                if (bodyText != null) bodyText.text = line.text;
                yield return new WaitForSeconds(line.seconds);
            }
            if (group != null) group.alpha = 0;
        }
    }
}
