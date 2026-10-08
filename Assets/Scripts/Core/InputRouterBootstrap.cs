using UnityEngine;
using UnityEngine.InputSystem;

namespace Eoduk.Core
{
    public sealed class InputRouterBootstrap : MonoBehaviour
    {
        [SerializeField] private InputActionAsset actions;
        private void Awake() { if (GameManager.Instance == null) SaveSystem.Load(); InputRouter.Initialize(actions); }
    }
}
