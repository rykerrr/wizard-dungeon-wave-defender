using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace WizardGame.Spell_Creation
{
    public class SpellNameSetter : MonoBehaviour
    {
        [SerializeField] private TMP_InputField setNameInputField = default;
        [SerializeField] private SpellCreationHandler spellCreationHandler = default;

        private void Awake()
        {
            spellCreationHandler.onDefaultSpellNameChanged += (spellName) => 
                setNameInputField.SetTextWithoutNotify(spellName);
        }

        public void InputField_SetName(string spellName)
        {
            spellCreationHandler.SpellName = spellName;
        }
    }
}