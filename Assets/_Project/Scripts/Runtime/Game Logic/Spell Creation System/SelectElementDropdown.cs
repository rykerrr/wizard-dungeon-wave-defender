using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using WizardGame.Combat_System;
using WizardGame.Combat_System.Element_System;

namespace WizardGame.Spell_Creation
{
    public class SelectElementDropdown : MonoBehaviour
    {
        [SerializeField] private SpellCreationHandler spellCreationHandler = default;
        [SerializeField] private Character character = default;
        [SerializeField] private TMP_Dropdown dropdown = default;

        private List<Element> elements = new List<Element>();

        private void Awake()
        {
            dropdown ??= GetComponent<TMP_Dropdown>();

            InitDropdown();
        }

        private void InitDropdown()
        {
            elements.Clear();

            LoadElementsIntoDropdown();
            ForceDropdownSelect(0);

            // This works because it's actually a property, setting it simply calls its internal
            // IL SetValue method which prompts the onSelect
        }

        // because i'm lazy
        private void LoadElementsIntoDropdown()
        {
            elements = character.CharacterElement.ToList();

            List<TMP_Dropdown.OptionData> elementOptions = new List<TMP_Dropdown.OptionData>();

            foreach (var elem in elements)
            {
                var data = new TMP_Dropdown.OptionData(elem.Name, elem.ElementSprite);

                elementOptions.Add(data);
            }

            dropdown.AddOptions(elementOptions);
        }

        private void ForceDropdownSelect(int ind)
        {
            dropdown.value = ind;
            dropdown.onValueChanged?.Invoke(ind);
        }

        public void Dropdown_SelectElement(int localElId)
        {
            // Debug.Log("Got notified for: " + localElId);

            spellCreationHandler.SpellElement = elements[localElId];
        }

        [ContextMenu("Reload Elements From Referenced Character")]
        public void ReloadElementsFromReferencedCharacter()
        {
            InitDropdown();
        }
    }
}