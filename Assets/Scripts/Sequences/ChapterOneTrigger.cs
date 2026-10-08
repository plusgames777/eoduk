using UnityEngine;

namespace Eoduk.Sequences
{
    public sealed class ChapterOneTrigger : MonoBehaviour
    {
        public enum Kind { Chase, Return }
        [SerializeField] private ChapterOneFlow flow;
        [SerializeField] private Kind kind;
        public void Initialize(ChapterOneFlow chapterFlow, Kind triggerKind) { flow = chapterFlow; kind = triggerKind; }
        public void ResetTrigger() { gameObject.SetActive(true); }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || flow == null) return;
            if (kind == Kind.Chase) flow.BeginChase(); else flow.BeginReturn();
            gameObject.SetActive(false);
        }
    }
}
