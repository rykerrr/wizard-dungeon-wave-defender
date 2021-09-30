using System;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.CustomEventSystem;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Utility.Infrastructure.Factories;

namespace WizardGame.Item_System.World_Interaction
{
    public class PlayerInteractionBehavior : MonoBehaviour
    {
        [SerializeField] private Inventory inventory = default;
        [SerializeField] private Interactor interactor = default;

        private PhysicalItemInteractions physInteractions = default;
        StringBuilder sb;

        private void Awake()
        {
            physInteractions = new PhysicalItemInteractions(inventory.ItemContainer);
            sb = new StringBuilder();
        }

        private void Update()
        {
            var hits = interactor.FindInteractables(transform.position, transform.forward);

            if (hits.Length == 0) return;

            // TODO: Convert to method and use Input Actions
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                Debug.Log("Pressed");
                
                physInteractions.TryPickupItems(hits);
            }
        }

        public void ThrowPhysicalItem(ItemThrowData data)
        {
            physInteractions.ThrowPhysicalItem(data);
        }
        
        [ContextMenu("Test PhysicalItemFactory.CreateInstance")]
        private void CreateInstanceTest()
        {
            PhysicalItemFactory.CreateInstance(Vector3.zero, Quaternion.identity, null);
        }
    }
}