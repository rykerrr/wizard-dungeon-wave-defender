using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WizardGame.Stats_System
{
    public static class StatTypeFactory
    {
        private static Dictionary<string, StatType> types = new Dictionary<string, StatType>();
        
        // possibly change to use Assembly and reflection
        private static string locationInResources = "Game Data/Stat System/Stat Types";

        static StatTypeFactory()
        {
            LoadStatTypes();
        }

        private static void LoadStatTypes()
        {
            var typesToLoad = Resources.LoadAll<StatType>(locationInResources);

            foreach (var type in typesToLoad)
            {
                types.Add(type.Name, type);
            }
        }

        public static StatType GetType(string typeName)
        {
            if (!types.ContainsKey(typeName))
            {
                Debug.Log(typeName);

                foreach (var kvp in types)
                {
                    Debug.Log($"{kvp.Key} | {kvp.Value}");
                }
                
#if UNITY_EDITOR
                Debug.LogWarning("StatType wasn't loaded." +
                                 " Perhaps it doesn't exist in the folder?");
#endif

                return null;
            }
            
            return types[typeName];
        }
    }
}