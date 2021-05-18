using UnityEngine;
using WizardGame.Item_System.Items;

namespace ItemSystem.World_Interaction
{
    public class PhysicalItem : MonoBehaviour
    {
        [SerializeField] private InventoryItem targetItem;

        public InventoryItem TargetItem => targetItem;

        public void Init(InventoryItem itemToInitialize)
        {
            // loading logic, changing the bilboard/mesh/etc...
        }
    }
}