using UnityEngine;

namespace WizardGame.Item_System.Item_Containers
{
    public class LocalInventoryBehaviour : MonoBehaviour
    {
        [SerializeField] private Inventory inventoryToLoad;

        [SerializeReference] private ItemContainer localItemContainer;

        public ItemContainer LocalItemContainer => localItemContainer;

        private void Awake()
        {
            localItemContainer = new ItemContainer(inventoryToLoad.ItemContainer);
        }
    }
}