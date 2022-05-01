using System.IO;
using UnityEngine;

namespace Saves
{
    public class SaveManager : MonoBehaviour
    {
        private static string _saveFile;
        public static readonly SaveState ActiveState = new SaveState();

        private void Awake()
        {
            print(Application.persistentDataPath);
            _saveFile = Application.persistentDataPath + "/save.rogue";
            LoadState();
        }

        public static void SaveState()
        {
            // save active state to a file
            var json = JsonUtility.ToJson(ActiveState);
            File.WriteAllText(_saveFile, json);
        }

        private static void LoadState()
        {
            // make new save file if one doesn't exist
            if (!File.Exists(_saveFile))
            {
                print($"save file at: {_saveFile} doesn't exist");
                print("initialising save state");
                SaveState();
                return;
            }
            // otherwise load the save file
            var json = File.ReadAllText(_saveFile);
            JsonUtility.FromJsonOverwrite(json, ActiveState);
        }
    }
}