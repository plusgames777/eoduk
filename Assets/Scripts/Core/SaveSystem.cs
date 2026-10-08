using System;
using System.IO;
using UnityEngine;

namespace Eoduk.Core
{
    public static class SaveSystem
    {
        private static string SavePath => Path.Combine(Application.persistentDataPath, "save.json");
        private static string SettingsPath => Path.Combine(Application.persistentDataPath, "settings.json");
        public static SaveData Data { get; private set; } = new SaveData();
        public static SettingsData Settings { get; private set; } = new SettingsData();

        public static void Load()
        {
            Data = Read(SavePath, new SaveData());
            Settings = Read(SettingsPath, new SettingsData());
        }

        public static void Save() => Write(SavePath, Data);
        public static void SaveSettings() => Write(SettingsPath, Settings);

        private static T Read<T>(string path, T fallback) where T : class
        {
            try { return File.Exists(path) ? JsonUtility.FromJson<T>(File.ReadAllText(path)) ?? fallback : fallback; }
            catch (Exception e) { Debug.LogWarning($"Save data could not be read ({Path.GetFileName(path)}): {e.Message}"); return fallback; }
        }

        private static void Write<T>(string path, T value)
        {
            try { File.WriteAllText(path, JsonUtility.ToJson(value, true)); }
            catch (Exception e) { Debug.LogError($"Could not save {Path.GetFileName(path)}: {e.Message}"); }
        }
    }
}
