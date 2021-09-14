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

        private string customSpellName = default;
        private string spellName = default;
        
        public event Action<string> onDefaultSpellNameChanged = delegate { };
        public event Action<Type> onSpellCreated = delegate { };
        
        public string SpellName
        {
            get => spellName;
            set
            {
                if (spellName == value) return;
                
                spellName = value;
                customSpellName = spellName;
            }
        }
        
        public SpellBookItem SpellFoundation
        {
            get => spellFoundation;
            set
            {
                spellFoundation = value;
                
                if (string.IsNullOrWhiteSpace(customSpellName))
                {
                    UpdateDefaultSpellName();
                }
            }
        }

        public BaseSpellCastData Data
        {
            get => data;
            set => data = value;
        }

        public Element SpellElement
        {
            get => spellElement;
            set
            {
                spellElement = value;
                
                if (string.IsNullOrWhiteSpace(customSpellName))
                {
                    UpdateDefaultSpellName();
                }
            }
        }

        private void UpdateDefaultSpellName()
        {
            var nameSpellBook = "";
            var nameElement = ReferenceEquals(SpellElement, null) ? "" : SpellElement.Name;
            
            if (!ReferenceEquals(SpellFoundation, null))
            {
                var sfName = SpellFoundation.Name;
                
                if (sfName.Contains("Foundation"))
                {
                    sfName = sfName.Remove(sfName.IndexOf("Foundation", StringComparison.Ordinal),
                        "Foundation".Length);
                }

                nameSpellBook = sfName;
            }
            
            spellName = $"{nameElement} {nameSpellBook}";

            onDefaultSpellNameChanged?.Invoke(spellName);
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
            var spellKey = (spellFoundation.SpellPrefab.GetType(), SpellElement);

            var spellExists = SpellFactory.Spells.ContainsKey(spellKey);

            if (!spellExists)
            {
                foreach (var spell in SpellFactory.Spells)
                {
                    Debug.LogWarning(spell.Key.Item1 + "  " + spell.Key.Item2 + " " + spell.Value.name
                     + " " + spell.Value.SpellElement);
                }

                Debug.LogError("Spell doesn't exist\n" + spellKey.Item1 + "  " + spellKey.Item2);
                return null;
            }
            
            var newItem = (SpellBookItem) ScriptableObject.CreateInstance(spellFoundation.GetType());
            
            newItem.Init(SpellName, SpellElement.ElementSprite);
            newItem.InitCooldown(spellFoundation.CooldownDuration);
            newItem.Init(spellFoundation.Rarity, spellFoundation.SellPrice, spellFoundation.MaxStack);
            newItem.Init(spellFoundation.SpellCastPrefab, spellFoundation.SpellCirclePrefab, data
                , SpellFactory.Spells[spellKey]);

            newItem.ItemUseEvent = spellFoundation.ItemUseEvent;
            
            var spellId = Guid.NewGuid();

            // data = (BaseSpellCastData) Activator.CreateInstance(data.GetType());
            // will probably need to re-call SelectMenu in ChangeSpellCreationData
            
            SpellName = "";
            UpdateDefaultSpellName();

            onSpellCreated?.Invoke(data.GetType());
            
            return newItem;
        }
    }
}
