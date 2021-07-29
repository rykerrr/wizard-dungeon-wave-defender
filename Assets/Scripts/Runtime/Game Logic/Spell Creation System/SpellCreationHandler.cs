using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Combat_System;
using WizardGame.Item_System;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Item_System.Items;

namespace WizardGame.Spell_Creation
{
    public class SpellCreationHandler : MonoBehaviour
    {
        [SerializeField] private Inventory targetInventory = default;

        private SpellBookItem spellFoundation = default;
        private BaseSpellCastData data = default;

        public SpellBookItem SpellFoundation
        {
            get => spellFoundation;
            set => spellFoundation = value;
        }

        public BaseSpellCastData Data
        {
            get => data;
            set => data = value;
        }

        public SpellBookItem OnClick_TryCreateSpellBook()
        {
            var spell = spellFoundation;
            var newItem = (SpellBookItem) ScriptableObject.CreateInstance(spell.GetType());

            newItem.Init(spell.SpellCastPrefab, spell.SpellCirclePrefab, data);
            newItem.Init(spell.Rarity, spell.SellPrice, spell.MaxStack);
            newItem.Init(spell.name, spell.Icon);
            newItem.ItemUseEvent = spell.ItemUseEvent;
            
            var spellId = Guid.NewGuid();
            
            if (newItem == null) return null;

            targetInventory.ItemContainer.AddItem(new ItemSlot(newItem, 1));

            return newItem;
        }
    }
}
