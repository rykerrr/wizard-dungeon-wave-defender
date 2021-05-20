using UnityEngine;
using WizardGame.Item_System;
using WizardGame.Item_System.Item_Containers;

namespace ItemSystem.World_Interaction
{
    public class PhysicalItemPickup
    {
        private IItemContainer itemContainer = default;

        public IItemContainer ItemContainer
        {
            get => itemContainer;
            set => itemContainer = value;
        }

        public PhysicalItemPickup(IItemContainer itemContainer)
        {
            this.itemContainer = itemContainer;
        }

        public void TryPickupItems(Collider[] possibleItems)
        {
            foreach (var collider in possibleItems)
            {
                PhysicalItem item;

                if ((item = collider.GetComponent<PhysicalItem>()) == null) continue;

                itemContainer.AddItem(new ItemSlot(item.TargetItem, 1));
                item.gameObject.SetActive(false);
            }
        }
    }
}