using System;
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

        private StringBuilder sb = new StringBuilder();

        public CharacterData(Character character)
        {
            elements = character.CharacterElement;
        }

        public CharacterData(Element[] elements)
        {
            this.elements = elements;
        }

        public static implicit operator CharacterData(Character character)
        {
            return new CharacterData(character);
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