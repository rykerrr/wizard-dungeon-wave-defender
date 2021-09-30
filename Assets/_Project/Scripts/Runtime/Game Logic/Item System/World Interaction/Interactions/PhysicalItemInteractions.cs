using System.Linq;
using UnityEngine;
using WizardGame.Item_System;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Item_System.Items;
using WizardGame.CustomEventSystem;

namespace WizardGame.Item_System.World_Interaction
{
    public class PhysicalItemInteractions : IInteraction
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

        public bool TryInteract(IInteractable obj)
        {
            if (!(obj is InteractablePhysicalItem physItem)) return false;
            
            itemContainer.AddItem(new ItemSlot(physItem.TargetItem, 1));
            physItem.gameObject.SetActive(false);
            
            return true;
        }
    }
}