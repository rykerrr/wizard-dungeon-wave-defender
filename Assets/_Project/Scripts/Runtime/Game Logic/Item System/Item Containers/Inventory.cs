using UnityEngine;
using WizardGame.CustomEventSystem;
using WizardGame.Item_System.Items;
using WizardGame.Utility;

namespace WizardGame.Item_System.Item_Containers
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Item Containers/Inventory")]
    public class Inventory : ScriptableObjectAutoNameSet
    {
        [SerializeField] private VoidGameEvent onInventoryItemsUpdated = default;
        [SerializeField] private ItemContainer itemContainer = default;

        public ItemContainer ItemContainer => itemContainer;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            if (itemContainer == null  || onInventoryItemsUpdated == null) return;
            
            ItemContainer.OnItemsUpdated += onInventoryItemsUpdated.Raise;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            if (itemContainer == null || onInventoryItemsUpdated == null) return;

            ItemContainer.OnItemsUpdated -= onInventoryItemsUpdated.Raise;
        }

        #region editor methods
        
        #if UNITY_EDITOR
        
        [Header("Debug")]
        [SerializeField] private InventoryItem itemToAdd = default;
        [SerializeField] private int quantity;
        
        [ContextMenu("Test add")]
        public void AddItem()
        {
            ItemContainer.AddItem(new ItemSlot(itemToAdd, quantity));
        }
        
        #endif
        
        #endregion
    }
}