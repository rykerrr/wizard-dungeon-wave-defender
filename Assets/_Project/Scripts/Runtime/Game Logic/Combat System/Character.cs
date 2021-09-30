using UnityEngine;
using WizardGame.Combat_System.Element_System;
using WizardGame.MainMenu;

namespace WizardGame.Combat_System
{
    // Naming of the class may not be perfect..
    // Will contain team-based data in the future
    public class Character : MonoBehaviour
    {
        private Element[] characterElements = default;

        public Element[] CharacterElement => characterElements;

        public void InitCharacterData(CharacterData data)
        {
            characterElements = data.Elements;
        }
    }
}