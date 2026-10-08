using System;
using UnityEngine;

namespace Eoduk.UI
{
    [Serializable] public sealed class DialogueLine { public string speaker; [TextArea] public string text; public float seconds = 3f; }
}
