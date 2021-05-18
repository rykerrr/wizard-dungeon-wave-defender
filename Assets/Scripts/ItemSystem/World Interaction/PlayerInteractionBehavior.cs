using System;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.CustomEventSystem;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Utility.Infrastructure.Factories;

namespace ItemSystem.World_Interaction
{
    public class PlayerInteractionBehavior : MonoBehaviour
    {
        [SerializeField] private Inventory inventory = default;
        [SerializeField] private Interactor interactor = default;

        private PhysicalItemPickup pickup = default;
        
        private void Awake()
        {
            pickup = new PhysicalItemPickup(inventory.ItemContainer);
        }

        private void Update()
        {
            Collider[] hits = interactor.FindInteractables(transform.position, transform.forward);

            if (hits.Length == 0) return;

            // TODO: Convert to method and use Input Actions
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                pickup.TryPickupItems(hits);
            }
        }
        
        [ContextMenu("Test PhysicalItemFactory.CreateInstance")]
        private void CreateInstanceTest()
        {
            PhysicalItemFactory.CreateInstance(Vector3.zero, Quaternion.identity, null);
        }
    }
}