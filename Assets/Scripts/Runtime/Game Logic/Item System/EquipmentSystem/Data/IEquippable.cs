using WizardGame.Stats_System;

namespace WizardGame.Item_System.Equipment_system
{
    public interface IEquippable
    {
        EquipmentType EquipmentType { get; }

        StatType StatType { get; }
        StatModifier StatModifier { get; }
        
        void Equip();
        void UnEquip();
    }
}