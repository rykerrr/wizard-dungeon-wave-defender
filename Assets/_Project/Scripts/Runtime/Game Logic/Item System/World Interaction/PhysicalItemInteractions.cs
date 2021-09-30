using System.Linq;
using UnityEngine;
using WizardGame.Item_System;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Item_System.Items;
using WizardGame.CustomEventSystem;

namespace WizardGame.Item_System.World_Interaction
{
    public class PhysicalItemInteractions
    {
        private IItemContainer itemContainer = default;

        public IItemContainer ItemContainer
        {
            get => itemContainer;
            set => itemContainer = value;
        }

        public PhysicalItemInteractions(IItemContainer itemContainer)
        {
            this.itemContainer = itemContainer;
        }

        public void TryPickupItems(params Collider[] possibleItems)
        {
            foreach (var collider in possibleItems)
            {
                PhysicalItem physItem;

                if (ReferenceEquals(physItem = collider.GetComponent<PhysicalItem>(), null)) continue;

                itemContainer.AddItem(new ItemSlot((InventoryItem)physItem.TargetItem, 1));
                physItem.gameObject.SetActive(false);
            }
        }

        public void ThrowPhysicalItem(ItemThrowData data)
        {
            var physItem = data.PhysItem;
            
            var rb = physItem.GetComponent<Rigidbody>();
            
            rb.AddForce(data.ThrowForce, ForceMode.Impulse);
        }
    }
}