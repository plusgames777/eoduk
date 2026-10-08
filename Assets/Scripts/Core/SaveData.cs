using System;
using System.Collections.Generic;
using UnityEngine;

namespace Eoduk.Core
{
    [Serializable]
    public sealed class SaveData
    {
        public int chapterId = 1;
        public string checkpointId = "ch1_spawn";
        public int purifyPower;
        public List<ItemCount> items = new List<ItemCount>();
        public List<string> flags = new List<string>();
        public float cameraBatteryRemaining = 180f;
        public int beadLevel;
        public float playtimeSeconds;
        public List<string> journalUnlocked = new List<string>();
        public Vector3 checkpointPosition;
        public float checkpointYaw;
    }

    [Serializable]
    public sealed class ItemCount
    {
        public string id;
        public int count;
        public ItemCount(string itemId, int amount) { id = itemId; count = amount; }
    }

    [Serializable]
    public sealed class SettingsData
    {
        public float masterVolume = 1f;
        public float musicVolume = .8f;
        public float sfxVolume = 1f;
        public float voiceVolume = 1f;
        public bool headBob = true;
        public float mouseSensitivity = 1f;
        public string subtitleLanguage = "ko";
        public string bindingOverrides = "";
    }
}
