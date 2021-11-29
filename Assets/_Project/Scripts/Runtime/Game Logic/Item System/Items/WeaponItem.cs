using UnityEngine;
using WizardGame.Item_System.Items;

namespace WizardGame.Item_System
{
    [CreateAssetMenu(fileName = "New Weapon Item", menuName = "Items/New Weapon Item")]
    public class WeaponItem : EquippableItem
    {
        // Not merged with EquipmentItem as this'll contain data on how to physically equip a weapon
        // Equipment items won't be physically equippable though, they'll just add to stats

        public override void Equip()
        {
            ItemUseEvent.Raise(this);
        }

        public override void UnEquip()
        {
            onUnEquipItem.Raise(this);
        }
    }
}