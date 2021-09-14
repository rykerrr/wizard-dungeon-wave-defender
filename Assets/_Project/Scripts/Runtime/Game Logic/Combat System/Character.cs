using UnityEngine;
using WizardGame.Combat_System.Element_System;

namespace WizardGame.Combat_System
{
    // Naming of the class may not be perfect..
    // Will contain team-based data in the future
    public class Character : MonoBehaviour
    {
        [SerializeField] private Element[] characterElements = default;

        public Element[] CharacterElement => characterElements;
    }
}