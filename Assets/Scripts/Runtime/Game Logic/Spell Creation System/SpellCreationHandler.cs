using System;
using UnityEngine;
using WizardGame.Combat_System;
using WizardGame.Combat_System.Element_System;
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
        private Element spellElement = default;
        
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

        public Element SpellElement
        {
            get => spellElement;
            set => spellElement = value;
        }

        public void OnClick_TryCreateSpellBook()
        {
            var newItem = TryCreateSpellBook();

            if (!newItem)
            {
                Debug.LogError("NewItem null, huh?");
            
                return;
            }
            
            targetInventory.ItemContainer.AddItem(new ItemSlot(newItem, 1));
        }

        public SpellBookItem TryCreateSpellBook()
        {
            var spell = spellFoundation;
            var newItem = (SpellBookItem) ScriptableObject.CreateInstance(spell.GetType());
            
            newItem.Init(spell.name, spell.Icon);
            newItem.InitCooldown(spell.CooldownDuration);
            newItem.Init(spell.Rarity, spell.SellPrice, spell.MaxStack);
            newItem.Init(spell.SpellCastPrefab, spell.SpellCirclePrefab, data
                , spellElement);

            newItem.ItemUseEvent = spell.ItemUseEvent;
            
            var spellId = Guid.NewGuid();
            
            if (newItem == null) return null;

            return newItem;
        }
    }
}
