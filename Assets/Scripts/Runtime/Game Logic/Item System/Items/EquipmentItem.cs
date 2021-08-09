using UnityEngine;
using WizardGame.Item_System.Items;

namespace WizardGame.Item_System
{
    [CreateAssetMenu(fileName = "New Equipment Item", menuName = "Items/Equipment Item")]
    public class EquipmentItem : EquippableItem
    {
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
