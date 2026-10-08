using System.Collections;
using UnityEngine;
using Eoduk.Player;

namespace Eoduk.Sequences
{
    public sealed class BeadHandoverSequence : MonoBehaviour
    {
        [SerializeField] private BeadLight bead;
        [SerializeField] private GameObject pickup;
        [SerializeField] private float pauseSeconds = .6f;
        public void Play() { StartCoroutine(Handover()); }
        private IEnumerator Handover() { yield return new WaitForSeconds(pauseSeconds); if (pickup != null) pickup.SetActive(false); if (bead != null) bead.SetOn(true); }
    }
}
