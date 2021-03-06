using UnityEngine;
using WizardGame.Combat_System;
using WizardGame.Combat_System.Element_System;

namespace WizardGame.Item_System.Items
{
    [CreateAssetMenu(fileName = "New Spell Book Item", menuName = "Items/New Spell Book Item")]
    public class SpellBookItem : InventoryItem
    {
        [Header("Spell properties")]
        [SerializeField] private SpellCastBase spellCastPrefab = default;
        [SerializeField] private SpellBase spellPrefab;
        [SerializeField] private CastPlaceholder spellCirclePrefab = default;
        [SerializeField] private Element spellElement = default;

        private BaseSpellCastData spellCastData = default;
        
        public SpellCastBase SpellCastPrefab => spellCastPrefab;
        public SpellBase SpellPrefab => spellPrefab;
        public CastPlaceholder SpellCirclePrefab => spellCirclePrefab;
        public BaseSpellCastData SpellCastData => spellCastData;
        public Element SpellElement => spellElement;

        public void Init(SpellCastBase spellCastPrefab, CastPlaceholder spellCirclePrefab
            , BaseSpellCastData spellCastData, SpellBase spellPrefab)
        {
            this.spellCastPrefab = spellCastPrefab;
            this.spellCirclePrefab = spellCirclePrefab;
            this.spellCastData = spellCastData;
            this.spellPrefab = spellPrefab;
            spellElement = spellPrefab.SpellElement;
        }

        public override string GetInfoDisplayText()
        {
            return $"Spell: {Name}\nElement: {SpellElement.Name}";
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