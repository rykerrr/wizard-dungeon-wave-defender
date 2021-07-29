using UnityEngine;
using WizardGame.CustomEventSystem;
using WizardGame.Item_System.Equipment_system;
using WizardGame.Item_System.Items;
using WizardGame.Stats_System;

namespace WizardGame.Item_System
{
    [CreateAssetMenu(fileName = "New Weapon Item", menuName = "Items/Weapon Item")]
    public class WeaponItem : InventoryItem, IEquippable
    {
        [SerializeField] private StatType statType = default;
        [SerializeField] private StatModifier modifier = default;
        
        [SerializeField] private HotbarItemGameEvent onUnEquipItem;

        public EquipmentType EquipmentType => EquipmentType.Weapon;
        
        public StatType StatType => statType;
        public StatModifier StatModifier => modifier;
        
        public void Equip()
        {
            ItemUseEvent.Raise(this);
        }

        public void UnEquip()
        {
            onUnEquipItem.Raise(this);
        }

        public override string GetInfoDisplayText()
        {
            sb.Clear();

            sb.Append("Type: ").Append(EquipmentType).Append(" Stat to modify: ").Append(statType).AppendLine();
            sb.Append("Stat Modifier: ").Append(StatModifier).AppendLine();

            return sb.ToString(); 
        }
    }
}