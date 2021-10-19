using UnityEngine;
using WizardGame.Combat_System.Cooldown_System;
using System.Linq;

namespace WizardGame.Item_System.Item_Containers
{
    public class LocalInventoryBehaviour : MonoBehaviour
    {
        [SerializeField] private Inventory inventoryToLoad;
        [SerializeField] private CooldownSystem cdSys;
        
        [SerializeReference] private ItemContainer localItemContainer;

        public ItemContainer LocalItemContainer => localItemContainer;

        private void Awake()
        {
            localItemContainer = new ItemContainer(inventoryToLoad.ItemContainer);

            cdSys.InitializeCooldowns(localItemContainer.ItemSlots.Where(x => !ReferenceEquals(x.invItem, null))
                .Select(x => (IHasCooldown)x.invItem.CooldownData).ToArray());
        }
    }
}