using System;
using Ludiq;
using UnityEngine;
using WizardGame.CustomEventSystem;
using WizardGame.Item_System.Equipment_system;
using WizardGame.Item_System.Items;
using WizardGame.Stats_System;

namespace WizardGame.Item_System
{
    [CreateAssetMenu(fileName = "New Equipment Item", menuName = "Items/Equipment Item")]
    public class EquipmentItem : InventoryItem, IEquippable
    {
        [SerializeField] private StatType statType = default;
        [SerializeField] private StatModifier modifierData = default;
        
        [SerializeField] private HotbarItemGameEvent onUnEquipItem;
        
        [SerializeField] private EquipmentType equipmentType;
        
        public EquipmentType EquipmentType => equipmentType;

        [NonSerialized] private StatModifier statModifier = default;
        
        public StatType StatType => statType;

        public StatModifier StatModifier
        {
            get
            {
                if (statModifier == null)
                {
                    statModifier = new StatModifier(modifierData.Type, modifierData.Value, Name);
                    Debug.Log(statModifier + " he he " + modifierData);
                }

                return statModifier;
            }
        }
        
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

            sb.Append("Type: ").Append(EquipmentType).Append(" Stat to modify: ").Append(statType.Name).AppendLine();
            sb.Append("Stat Modifier: ").Append(StatModifier).AppendLine();

            return sb.ToString();
        }
    }
}
