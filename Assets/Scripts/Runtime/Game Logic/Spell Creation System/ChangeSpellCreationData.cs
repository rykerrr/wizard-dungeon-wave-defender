using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Spell_Creation
{
    public class ChangeSpellCreationData : MonoBehaviour
    {
        [SerializeField] private SpellCreationHandler thisHandler;
        [SerializeField] private SpellCreationMenuBase thisMenu = default;
        [SerializeField] private Transform creationPanelsContainer = default;

        public void OnClick_SelectMenu()
        {
            CloseAllCreationPanels();
            SelectMenu();
        }

        private void SelectMenu()
        {
            thisHandler.SpellFoundation = thisMenu.SpellFoundation;
            thisHandler.Data = thisMenu.Data;

            thisMenu.gameObject.SetActive(true);
        }

        private void CloseAllCreationPanels()
        {
            foreach (Transform child in creationPanelsContainer)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}