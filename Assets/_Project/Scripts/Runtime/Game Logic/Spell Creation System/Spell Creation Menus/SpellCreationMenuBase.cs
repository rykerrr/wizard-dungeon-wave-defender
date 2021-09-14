using System;
using TMPro;
using UnityEngine;
using WizardGame.Combat_System;
using WizardGame.Item_System.Items;

namespace WizardGame.Spell_Creation
{
    // Would fit great as an interface...if they were serializable
    public abstract class SpellCreationMenuBase : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI manaCostLabel = default;

        protected virtual void Awake()
        {
            UpdateLabels();
        }

        public abstract SpellBookItem SpellFoundation { get; }
        public abstract BaseSpellCastData Data { get; }

        public abstract void UpdateLabels();
    }
}
