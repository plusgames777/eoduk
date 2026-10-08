using UnityEngine;
using Eoduk.Core;

namespace Eoduk.UI
{
    public sealed class SettingsMenu : MonoBehaviour
    {
        public void SetMasterVolume(float value) { SaveSystem.Settings.masterVolume = Mathf.Clamp01(value); SaveSystem.SaveSettings(); }
        public void SetMusicVolume(float value) { SaveSystem.Settings.musicVolume = Mathf.Clamp01(value); SaveSystem.SaveSettings(); }
        public void SetSfxVolume(float value) { SaveSystem.Settings.sfxVolume = Mathf.Clamp01(value); SaveSystem.SaveSettings(); }
        public void SetSensitivity(float value) { SaveSystem.Settings.mouseSensitivity = Mathf.Clamp(value, .1f, 3f); SaveSystem.SaveSettings(); }
        public void SetHeadBob(bool value) { SaveSystem.Settings.headBob = value; SaveSystem.SaveSettings(); }
    }
}
