using UnityEngine;
using WizardGame.Item_System.Items;
using WizardGame.Stats_System;

namespace WizardGame.Item_System.Equipment_system
{
    public class EquipHandler : MonoBehaviour
    {
        [SerializeField] private StatsSystemBehaviour statsSystemBehaviour = default;

        // Probably not the best way to handle this but it works

        private StatsSystem statsSystem = default;

        private void Awake()
        {
            statsSystem = statsSystemBehaviour.StatsSystem;
        }

        public void ProcessEquip(HotbarItem item)
        {
            var equippable = (IEquippable) item;
            if (ReferenceEquals(equippable, null)) return;
            
            Debug.Log("Processing equip");
            statsSystem.AddModifierTo(equippable.StatType, equippable.StatModifier);
        }

        public void ProcessUnEquip(HotbarItem item)
        {
            var equippable = (IEquippable) item;
            if (ReferenceEquals(equippable, null)) return;
            
            Debug.Log("Processing unequip");
            statsSystem.RemoveModifierFrom(equippable.StatType, equippable.StatModifier);
        }
    }
}