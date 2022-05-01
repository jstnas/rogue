using System;
using System.Collections.Generic;

namespace Saves
{
    [Serializable]
    public class SaveState
    {
        // TODO: keep track of turns
        private List<string> unlockedLevels;

        public SaveState()
        {
            // level1 always unlocked
            unlockedLevels = new List<string> {"Scenes/Level1"};
        }

        public void UnlockLevel(string level)
        {
            // stop if level already unlocked
            if (unlockedLevels.Contains(level))
                return;
            unlockedLevels.Add(level);
            SaveManager.SaveState();
        }

        public bool LevelUnlocked(string level)
        {
            return unlockedLevels.Contains(level);
        }
    }
}