using System;
using UnityEngine.InputSystem;

namespace Eoduk.Core
{
    public static class InputRouter
    {
        private static InputActionAsset asset;
        private static InputActionMap player;
        private static InputActionMap ui;

        public static void Initialize(InputActionAsset actions)
        {
            if (asset == actions && player != null) return;
            if (asset != null) asset.Disable();
            asset = actions;
            player = asset != null ? asset.FindActionMap("Player", false) : null;
            ui = asset != null ? asset.FindActionMap("UI", false) : null;
            if (asset != null) asset.Enable();
            string overrides = SaveSystem.Settings?.bindingOverrides;
            if (asset != null && !string.IsNullOrEmpty(overrides))
            {
                try { asset.LoadBindingOverridesFromJson(overrides); }
                catch (Exception e) { UnityEngine.Debug.LogWarning($"Input bindings could not be restored: {e.Message}"); }
            }
        }

        public static InputAction Find(string name, bool fromUI = false) => (fromUI ? ui : player)?.FindAction(name, false);
        public static bool Pressed(string name) => Find(name)?.WasPerformedThisFrame() ?? false;
        public static bool Released(string name) => Find(name)?.WasReleasedThisFrame() ?? false;
        public static bool Held(string name) => Find(name)?.IsPressed() ?? false;
        public static UnityEngine.Vector2 Vector2(string name) => Find(name)?.ReadValue<UnityEngine.Vector2>() ?? UnityEngine.Vector2.zero;
        public static float Float(string name) => Find(name)?.ReadValue<float>() ?? 0f;
        public static string SaveOverrides()
        {
            string json = asset?.SaveBindingOverridesAsJson() ?? "";
            if (SaveSystem.Settings != null) { SaveSystem.Settings.bindingOverrides = json; SaveSystem.SaveSettings(); }
            return json;
        }
    }
}
