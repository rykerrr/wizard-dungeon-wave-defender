using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WizardGame.Combat_System.Element_System;
using WizardGame.MainMenu;
using WizardGame.MainMenu.HelperTests;

namespace WizardGame.SelectionWindow
{
    public class ElementSelectionConfirmPopup : MonoBehaviour
    {
        [Header("Preferences")]
        [SerializeField] private string selectionConfirmMessage = "Are you sure you'd like to select ";
        [SerializeField] [Multiline] private string warnMessage = default;

        [Header("References")] [SerializeField]
        private TextMeshProUGUI popupText = default;
        [SerializeField] private Image elementIconImage = default;
        [SerializeField] private SceneLoadController sceneLoad = default;
        
        private Element element = default;
        
        public void UpdateUI(Element element)
        {
            this.element = element;
            
            popupText.text = $"{selectionConfirmMessage} the {element.Name} element?\n {warnMessage}";

            elementIconImage.sprite = element.ElementSprite;
        }

        public void OnClick_ConfirmSelection()
        {
            Debug.Log($"Load scene with character data with {element} and save as character data");

            var newCharData = new CharacterData(element);

            OnSceneLoadedCharacterDataLoad.LoadedCharacterData = newCharData;
            JSONSaveManager.SaveCharacterDataFileToSelectedSlot(newCharData.Data);
            
            sceneLoad.LoadScene();
        }

        public void OnClick_CancelSelection()
        {
            gameObject.SetActive(false);

            element = null;
        }
    }
}