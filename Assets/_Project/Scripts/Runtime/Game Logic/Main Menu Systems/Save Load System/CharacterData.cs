using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WizardGame.Combat_System;
using WizardGame.Combat_System.Element_System;

namespace WizardGame.MainMenu
{
    [Serializable]
    public class CharacterData
    {
        [SerializeField] private Element[] elements = default;

        private CharacterSaveData data;

        public CharacterSaveData Data => data;
        
        public Element[] Elements => elements;
        
        private StringBuilder sb = new StringBuilder();

        public CharacterData(Character character)
        {
            elements = character.CharacterElement;

            data = new CharacterSaveData(elements);
        }

        public CharacterData(params Element[] elements)
        {
            this.elements = elements;
            
            data = new CharacterSaveData(elements);
        }

        public static implicit operator CharacterData(Character character)
        {
            return new CharacterData(character);
        }

        public static implicit operator CharacterData(CharacterSaveData data)
        {
            var elems = new List<Element>();
            
            foreach (var elName in data.ElNames)
            {
               elems.Add(ElementDB.GetElementByName(elName)); 
            }

            return new CharacterData(elems.ToArray());
        }

        public override string ToString()
        {
            if (ReferenceEquals(sb, null)) sb = new StringBuilder();

            sb.Clear();

            foreach (var elem in elements)
            {
                sb.Append(elem.ToString()).AppendLine();
            }

            return sb.ToString();
        }
    }
}