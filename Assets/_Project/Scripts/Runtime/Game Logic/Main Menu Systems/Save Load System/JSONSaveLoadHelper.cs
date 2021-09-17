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
        [SerializeField] private string saveFileName = "saveSlot1";
        
        [SerializeField] private Element[] elements;

        private void Update()
        {
            if(Keyboard.current.qKey.wasPressedThisFrame) Debug.Log(Application.persistentDataPath);
            
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                var charData = new CharacterData(elements);
                
                JSONSaveManager.SaveCharacterDataFile(saveFileName, charData);

                Debug.Log($"Saved: {charData.ToString()}");
            }

            if (Keyboard.current.backspaceKey.wasPressedThisFrame)
            {
                var charData = JSONSaveManager.LoadCharacterDataFile(saveFileName);

                Debug.Log($"Loaded: {charData.ToString()}");
            }
        }
    }
}