using ItemSystem.World_Interaction;
using UnityEngine;
using WizardGame.Item_System.Items;

namespace WizardGame.Utility.Infrastructure.Factories
{
    public static class PhysicalItemFactory
    {
        private static PhysicalItem basePhysicalItemPrefab = default;

        static PhysicalItemFactory()
        {
            basePhysicalItemPrefab = Resources.Load<PhysicalItem>("Factory Prefabs/PhysicalItemPrefab");
            
            if(basePhysicalItemPrefab == null) Debug.LogWarning("No physical item found at .../Assets/Resources/Factory Prefabs/PhysicalItemPrefab");
        }

        public static PhysicalItem CreateInstance(Vector3 position, Quaternion rotation, InventoryItem itemBase, Transform parent = null)
        {
            var retItem = Object.Instantiate(basePhysicalItemPrefab, position, rotation, parent);
            retItem.Init(itemBase); // Logic for setting the object specifics such as icon, etc, is handled by the physicalitem itself

            return retItem;
        }
    }
}