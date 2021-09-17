using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Combat_System.Element_System;

namespace WizardGame.MainMenu
{
    public class JSONSaveLoadHelper : MonoBehaviour
    {
        [SerializeField] private string saveFile = "saveFile1";
        [SerializeField] private string extension = "json";
        
        [SerializeField] private Element[] elements;

        private void Update()
        {
            if(Keyboard.current.qKey.wasPressedThisFrame) Debug.Log(Application.persistentDataPath);
            
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                var charData = new CharacterData(elements);
                var sfWithExt = $"{saveFile}.{extension}";
                
                JSONSaveManager.SaveCharacterDataFile(sfWithExt, charData);

                Debug.Log($"Saved: {charData.ToString()}");
            }

            if (Keyboard.current.backspaceKey.wasPressedThisFrame)
            {
                var sfWithExt = $"{saveFile}.{extension}";
                var charData = JSONSaveManager.LoadCharacterDataFile(sfWithExt);

                Debug.Log($"Loaded: {charData.ToString()}");
            }
        }
    }
}