using System;
using UnityEngine;

namespace Eoduk.Core
{
    public enum DeathCause { Eoduksini, Environment, Boss }

    public static class GameEvents
    {
        public static event Action<string> CheckpointReached;
        public static event Action<DeathCause> PlayerDied;
        public static event Action<int> PlayerHit;
        public static event Action<Vector3, float> NoiseMade;
        public static event Action<bool> WindGust;
        public static event Action<string> Purified;
        public static event Action<string> BossExorcised;
        public static event Action<string, int> ItemAcquired;
        public static event Action<int> SealPlaced;
        public static event Action<bool> ChaseChanged;
        public static event Action<string> DialogueRequested;
        public static event Action<int> ChapterCompleted;

        public static void RaiseCheckpointReached(string id) => CheckpointReached?.Invoke(id);
        public static void RaisePlayerDied(DeathCause cause) => PlayerDied?.Invoke(cause);
        public static void RaisePlayerHit(int remaining) => PlayerHit?.Invoke(remaining);
        public static void RaiseNoise(Vector3 position, float radius) => NoiseMade?.Invoke(position, radius);
        public static void RaiseWindGust(bool active) => WindGust?.Invoke(active);
        public static void RaisePurified(string id) => Purified?.Invoke(id);
        public static void RaiseBossExorcised(string id) => BossExorcised?.Invoke(id);
        public static void RaiseItemAcquired(string id, int count) => ItemAcquired?.Invoke(id, count);
        public static void RaiseSealPlaced(int index) => SealPlaced?.Invoke(index);
        public static void RaiseChaseChanged(bool active) => ChaseChanged?.Invoke(active);
        public static void RequestDialogue(string id) => DialogueRequested?.Invoke(id);
        public static void CompleteChapter(int chapter) => ChapterCompleted?.Invoke(chapter);
    }
}
