using UnityEngine;
using WizardGame.CustomEventSystem;
using WizardGame.Item_System.Items;

namespace WizardGame.Item_System.Item_Containers
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Item Containers/Inventory")]
    public class Inventory : ScriptableObject
    {
        [SerializeField] private VoidGameEvent onInventoryItemsUpdated = default;
        [SerializeField] private ItemContainer itemContainer = default;

        public ItemContainer ItemContainer => itemContainer;

        public void OnEnable()
        {
            if (itemContainer == null  || onInventoryItemsUpdated == null) return;
            
            ItemContainer.OnItemsUpdated += onInventoryItemsUpdated.Raise;
        }

        public void OnDisable()
        {
            if (itemContainer == null || onInventoryItemsUpdated == null) return;

            ItemContainer.OnItemsUpdated -= onInventoryItemsUpdated.Raise;
        }

        #region debug
        [Header("Debug")]
        [SerializeField] private InventoryItem itemToAdd = default;
        [SerializeField] private int quantity;
        
        [ContextMenu("Test add")]
        public void AddItem()
        {
            ItemContainer.AddItem(new ItemSlot(itemToAdd, quantity));
        }
        #endregion
    }
}