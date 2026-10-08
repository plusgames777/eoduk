using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Eoduk.Core
{
    public sealed class BootLoader : MonoBehaviour
    {
        [SerializeField] private string firstScene = "Ch1_Intro";
        [SerializeField] private float holdSeconds = .5f;
        private IEnumerator Start() { yield return new WaitForSecondsRealtime(holdSeconds); SceneManager.LoadScene(firstScene); }
    }
}
