﻿using UnityEngine;
using WizardGame.Combat_System;

namespace WizardGame.Item_System.Items
{
    [CreateAssetMenu(fileName = "New Spell Book Item", menuName = "Items/Spell Book Item")]
    public class SpellBookItem : InventoryItem
    {
        [SerializeField] private SpellCastBase spellCastPrefab = default;
        [SerializeField] private CastPlaceholder spellCirclePrefab = default;

        private BaseSpellCastData spellCastData = default;
        
        public SpellCastBase SpellCastPrefab => spellCastPrefab;
        public CastPlaceholder SpellCirclePrefab => spellCirclePrefab;

        public BaseSpellCastData SpellCastData => spellCastData;

        public void Init(SpellCastBase spellCastPrefab, CastPlaceholder spellCirclePrefab, BaseSpellCastData spellCastData)
        {
            this.spellCastPrefab = spellCastPrefab;
            this.spellCirclePrefab = spellCirclePrefab;
            this.spellCastData = spellCastData;
        }
        
        public SpellBookItem() : base() { }

        public override string GetInfoDisplayText()
        {
            return $"Spell: {Name}";
        }

        public override string ToString()
        {
            base.ToString();

            sb.Append("Spell Book | Cast Prefab: ").Append(SpellCastPrefab.name).Append(", Spell Circle: ")
                .Append(SpellCirclePrefab.name).Append(", Spell Cast Data =>").AppendLine()
                .Append(SpellCastData);

            return sb.ToString();
        }
    }
}