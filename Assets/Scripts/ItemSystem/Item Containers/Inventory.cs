using System;
using UnityEngine;
using WizardGame.CustomEventSystem;

namespace WizardGame.ItemSystem.Item_Containers
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Item Containers/Inventory")]
    public class Inventory : ScriptableObject
    {
        [SerializeField] private VoidGameEvent onInventoryItemsUpdated = null;
        
        public ItemContainer ItemContainer { get; } = new ItemContainer(20);

        public void OnEnable()
        {
            ItemContainer.OnItemsUpdated += onInventoryItemsUpdated.Raise;
        }

        public void OnDisable()
        {
            ItemContainer.OnItemsUpdated -= onInventoryItemsUpdated.Raise;
        }
    }
}