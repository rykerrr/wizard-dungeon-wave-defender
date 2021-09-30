using UnityEngine;
using System.IO;
using System.Linq;
using WizardGame.Combat_System.Element_System;

namespace WizardGame.MainMenu
{
    public static class JSONSaveManager
    {
        private static string saveDirLocation = "SaveData";
        private static string saveFileExtension = "json";

        public static string selectedSaveSlot;
        
        public static CharacterSaveData LoadCharacterDataFile(string saveSlotName)
        {
            var dirPath = Path.Combine(Application.persistentDataPath, saveDirLocation);
            var filePath = $"{Path.Combine(dirPath, saveSlotName)}.{saveFileExtension}";

            Debug.Log(dirPath + " | " + filePath + " | " + Application.persistentDataPath);

            if (!Directory.Exists(dirPath) || !File.Exists(filePath))
            {
                Debug.LogError("Attempted to load but dir or file does not exist.");
                Debug.Log($"{dirPath} | {filePath} | {saveSlotName}");
                Debug.Log($"{Directory.Exists(dirPath)} | {File.Exists(filePath)}");
                
                return null;
            }
            
            var jsonStr = File.ReadAllText(filePath);
            var loadedObj = JsonUtility.FromJson<CharacterSaveData>(jsonStr);

            Debug.Break();
            
            return loadedObj;
        }

        public static void SaveCharacterDataFileToSelectedSlot(CharacterSaveData data)
        {
            if (string.IsNullOrEmpty(selectedSaveSlot))
            {
                Debug.LogError($"Selected Save File is null or empty. ({selectedSaveSlot})");
                Debug.Break();

                return;
            }
            
            SaveCharacterDataFile(selectedSaveSlot, data);
        }

        public static void SaveCharacterDataFile(string saveSlotName, CharacterSaveData data)
        {
            var dirPath = Path.Combine(Application.persistentDataPath, saveDirLocation);
            
            Debug.Log($"{Application.persistentDataPath} | {saveDirLocation} | {dirPath}");

            if (!Directory.Exists(dirPath))
            {
                Debug.LogWarning("Attempting to load but dir does not exist, creating...");
                
                Directory.CreateDirectory(dirPath);
            }

            var json = JsonUtility.ToJson(data, true);

            var filePath = $"{Path.Combine(dirPath, saveSlotName)}.{saveFileExtension}";

            File.WriteAllText(filePath, json);
        }

        public static bool SaveFileExists(string saveSlotName)
        {
            var dirPath = Path.Combine(Application.persistentDataPath, saveDirLocation);

            if (!Directory.Exists(dirPath)) return false;

            var filePath = $"{Path.Combine(dirPath, saveSlotName)}.{saveFileExtension}";

            return File.Exists(filePath);
        }
    }
}