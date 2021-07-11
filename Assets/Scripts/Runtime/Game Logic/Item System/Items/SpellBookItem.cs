using UnityEngine;
using WizardGame.Combat_System;

namespace WizardGame.Item_System.Items
{
    [CreateAssetMenu(fileName = "New Spell Book Item", menuName = "Items/Spell Book Item")]
    public class SpellBookItem : InventoryItem
    {
        [SerializeField] private SpellCastBase spellCastPrefab = default;
        [SerializeField] private CastPlaceholder spellCirclePrefab = default;
        [SerializeField] private SpellCastData spellCastData = default;
        
        public SpellCastBase SpellCastPrefab => spellCastPrefab;
        public SpellCastData SpellCastData => spellCastData;
        public CastPlaceholder SpellCirclePrefab => spellCirclePrefab;
        
        public override string GetInfoDisplayText()
        {
            return $"Spell: {Name}";
        }
    }
}