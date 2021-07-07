using WizardGame.ItemSystem.World_Interaction;
using UnityEngine;
using WizardGame.Item_System.Items;

namespace WizardGame.Utility.Infrastructure.Factories
{
    public static class PhysicalItemFactory
    {
        private static PhysicalItem _basePhysicalItemDisplayPrefab = default;

        static PhysicalItemFactory()
        {
            _basePhysicalItemDisplayPrefab = Resources.Load<PhysicalItem>("Factory Prefabs/PhysicalItemPrefab");
            
            if(_basePhysicalItemDisplayPrefab == null) Debug.LogWarning("No physical item found at .../Assets/Resources/Factory Prefabs/PhysicalItemPrefab");
        }

        public static PhysicalItem CreateInstance(Vector3 position, Quaternion rotation, InventoryItem itemBase, Transform parent = null)
        {
            var retItem = Object.Instantiate(_basePhysicalItemDisplayPrefab, position, rotation, parent);
            retItem.InitDisplay(itemBase); // Logic for setting the object specifics such as icon, etc, is handled by the physicalitem itself

            return retItem;
        }
    }
}