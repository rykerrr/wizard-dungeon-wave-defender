using UnityEngine;

namespace WizardGame.Item_System.Items
{
    [CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable Item")]
    public class ConsumableItem : InventoryItem
    {
        public override void UseItem()
        {
            Debug.Log("Using consumable item");
            base.UseItem();
        }

        public override string GetInfoDisplayText()
        {
            return "Consume them";
        }
    }
}