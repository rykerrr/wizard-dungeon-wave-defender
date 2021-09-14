using System;
using UnityEngine;
using WizardGame.CustomEventSystem;
using WizardGame.Item_System.Equipment_system;
using WizardGame.Stats_System;

namespace WizardGame.Item_System.Items
{
    public abstract class EquippableItem : InventoryItem
    {
        [SerializeField] protected StatType statType = default;
        [SerializeField] protected StatModifier modifierData = default;
        [SerializeField] protected HotbarItemGameEvent onUnEquipItem;
        
        [NonSerialized] protected StatModifier statModifier = default;
        
        public EquipmentType EquipmentType => EquipmentType.Weapon;
        public StatType StatType => statType;
        
        public StatModifier StatModifier
        {
            get
            {
                if (statModifier == null)
                {
                    statModifier = new StatModifier(modifierData.Type, modifierData.Value, Name);
                }
                
                return statModifier;
            }
        }

        public abstract void Equip();
        public abstract void UnEquip();

        public override string GetInfoDisplayText()
        {
            sb.Clear();
            
            sb.Append("Type: ").Append(EquipmentType).Append(" Stat to modify: ").Append(statType).AppendLine();
            sb.Append("Stat Modifier: ").Append(StatModifier).AppendLine();

            return sb.ToString(); 
        }
    }
}