using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Combat_System.Element_System;

namespace WizardGame.MainMenu
{
    [Serializable]
    public class CharacterSaveData
    {
        [SerializeField] private List<string> elNames;

        public List<string> ElNames => elNames;
        
        public CharacterSaveData(params Element[] elements)
        {
            elNames = new List<string>();

            foreach (var el in elements)
            {
                elNames.Add(el.Name);   
            }
        }
    }
}