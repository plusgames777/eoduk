using System.Collections.Generic;
using UnityEngine;

namespace Eoduk.UI
{
    [CreateAssetMenu(menuName = "Eoduk/Dialogue/Table", fileName = "DLG_New")]
    public sealed class DialogueTable : ScriptableObject
    {
        public string id;
        public List<DialogueLine> lines = new List<DialogueLine>();
    }
}
