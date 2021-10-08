using WizardGame.Item_System.World_Interaction;
using UnityEngine;
using WizardGame.Item_System.Items;

namespace WizardGame.Item_System.World_Interaction
{
    public static class PhysicalItemFactory
    {
        private static InteractablePhysicalItem _baseInteractablePhysicalItemDisplayPrefab = default;

        static PhysicalItemFactory()
        {
            _baseInteractablePhysicalItemDisplayPrefab = Resources.Load<InteractablePhysicalItem>("Factory Prefabs/PhysicalItemPrefab");
            
            if(_baseInteractablePhysicalItemDisplayPrefab == null) Debug.LogWarning("No physical item found at .../Assets/Resources/Factory Prefabs/PhysicalItemPrefab");
        }

        public static InteractablePhysicalItem CreateInstance(Vector3 position, Quaternion rotation, InventoryItem itemBase, Transform parent = null)
        {
            var retItem = Object.Instantiate(_baseInteractablePhysicalItemDisplayPrefab, position, rotation, parent);
            retItem.Init(itemBase); // Logic for setting the object specifics such as icon, etc, is handled by the physicalitem itself

            return retItem;
        }
    }
}