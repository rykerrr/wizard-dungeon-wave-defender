using UnityEngine;
using System.IO;

namespace WizardGame.MainMenu
{
    public static class JSONSaveManager
    {
        private static string saveDirLocation = "SaveData";
        private static string saveFileExtension = "json";

        public static CharacterData LoadCharacterDataFile(string saveSlotName)
        {
            var dirPath = Path.Combine(Application.persistentDataPath, saveDirLocation);
            var filePath = $"{Path.Combine(dirPath, saveSlotName)}.{saveFileExtension}";

            if (!Directory.Exists(dirPath) || !File.Exists(filePath))
            {
                Debug.LogError("Attempted to load but dir or file does not exist.");
                Debug.Log($"{dirPath} | {filePath} | {saveSlotName}");
                Debug.Log($"{Directory.Exists(dirPath)} | {File.Exists(filePath)}");
                
                return null;
            }
            
            var jsonStr = File.ReadAllText(filePath);
            var loadedObj = JsonUtility.FromJson<CharacterData>(jsonStr);

            return loadedObj;
        }

        public static void SaveCharacterDataFile(string saveSlotName, CharacterData data)
        {
            var dirPath = Path.Combine(Application.persistentDataPath, saveDirLocation);
            
            Debug.Log($"{Application.persistentDataPath} | {saveDirLocation} | {dirPath}");

            if (!Directory.Exists(dirPath))
            {
                Debug.LogWarning("Attempting to load but dir does not exist, creating...");
                
                Directory.CreateDirectory(dirPath);
            }

            var json = JsonUtility.ToJson(data);

            var filePath = Path.Combine(dirPath, saveSlotName);

            // if (!File.Exists(filePath))
            // {
            //     File.Create(filePath);
            // }
            File.WriteAllText(filePath, json);
        }

        public static bool SaveFileExists(string saveSlotName)
        {
            var dirPath = Path.Combine(Application.persistentDataPath, saveDirLocation);

            if (!Directory.Exists(dirPath)) return false;

            var filePath = $"{Path.Combine(dirPath, saveSlotName)}.{saveFileExtension}";

            if (!File.Exists(filePath)) return false;

            return true;
        }
    }
}