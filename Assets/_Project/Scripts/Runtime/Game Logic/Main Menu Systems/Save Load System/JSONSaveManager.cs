using UnityEngine;
using System.IO;

namespace WizardGame.MainMenu
{
    public static class JSONSaveManager
    {
        private static string saveDirLocation = "SaveData";

        public static CharacterData LoadCharacterDataFile(string fileName)
        {
            var dirPath = Path.Combine(Application.persistentDataPath, saveDirLocation);
            var filePath = Path.Combine(dirPath, fileName);

            if (!Directory.Exists(dirPath) || !File.Exists(filePath))
            {
                Debug.LogError("Attempted to load but dir or file does not exist.");
                Debug.Log($"{dirPath} | {filePath} | {fileName}");
                Debug.Log($"{Directory.Exists(dirPath)} | {File.Exists(filePath)}");
                
                return null;
            }
            
            var jsonStr = File.ReadAllText(filePath);
            var loadedObj = JsonUtility.FromJson<CharacterData>(jsonStr);

            return loadedObj;
        }

        public static void SaveCharacterDataFile(string fileName, CharacterData data)
        {
            var dirPath = Path.Combine(Application.persistentDataPath, saveDirLocation);
            
            Debug.Log($"{Application.persistentDataPath} | {saveDirLocation} | {dirPath}");

            if (!Directory.Exists(dirPath))
            {
                Debug.LogWarning("Attempting to load but dir does not exist, creating...");
                
                Directory.CreateDirectory(dirPath);
            }

            var json = JsonUtility.ToJson(data);

            var filePath = Path.Combine(dirPath, fileName);

            // if (!File.Exists(filePath))
            // {
            //     File.Create(filePath);
            // }
            File.WriteAllText(filePath, json);
        }
    }
}