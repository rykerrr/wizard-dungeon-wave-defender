using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WizardGame.Managers
{
    public class ManagerManager : MonoBehaviour
    {
        public static ManagerManager Instance { get; private set; }

        private readonly Dictionary<Type, IManagerObject> managerDictionary = 
            new Dictionary<Type, IManagerObject>();

        public IManagerObject this[Type type] => managerDictionary[type];
        
        private void Awake()
        {
            Instance = this;
            
            LoadManagers();
        }

        private void LoadManagers()
        {
            var managers = GetComponents<IManagerObject>();
            
            foreach (var manager in managers)
            {
                managerDictionary.Add(manager.GetType(), manager);
            }
        }
        
        #region debug methods
        
        #if UNITY_EDITOR

        [ContextMenu("Log Manager Dictionary")]
        private void LogManagerDictionary()
        {
            foreach (var kvp in managerDictionary)
            {
                Debug.Log($"Key: {kvp.Key} Value: {kvp.Value}");
            }
        }

        [ContextMenu("Mimic LoadManagers Method Without Adding")]
        private void MimicLoadManagers()
        {
            var managers = GetComponents<IManagerObject>();

            foreach (var manager in managers)
            {
                Debug.Log($"Key: {manager.GetType()} Value: {manager}");
            }
        }
        
        #endif
        
        #endregion
    }
}