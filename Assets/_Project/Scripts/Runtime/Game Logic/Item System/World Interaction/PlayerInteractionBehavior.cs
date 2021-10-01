using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.CustomEventSystem;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Movement.Position;
using WizardGame.Utility.Infrastructure.Factories;

namespace WizardGame.Item_System.World_Interaction
{
    public class PlayerInteractionBehavior : MonoBehaviour
    {
        [SerializeField] private Inventory inventory = default;
        [SerializeField] private GetClosestInteractable getClosestInteractable = default;

        private readonly List<IInteraction> interactions = new List<IInteraction>();
        
        private StringBuilder sb;

        private void Awake()
        {
            interactions.Add(new PhysicalItemInteractions(inventory.ItemContainer));
            interactions.Add(new UIStandInteractions());
            
            sb = new StringBuilder();
        }

        private void Update()
        {
            // get nearest
            var hits = getClosestInteractable.FindInteractables(transform.position, transform.forward);
            
            if (hits == null) return;

            var nearestInteractable = hits[0].gameObject.GetComponent<IInteractable>();

            var tryInteract = InputMethod();
            // Debug.Log(tryInteract);
            // Debug.Log(hits[0].gameObject, hits[0]);
            
            if (!tryInteract) return;
                
            foreach (var interaction in interactions)
            {
                if (interaction.TryInteract(nearestInteractable)) break;
            }
        }

        private bool InputMethod()
        {
            var notPressedThisFrame = !Keyboard.current.eKey.wasPressedThisFrame;
            if (notPressedThisFrame) return false;
            
            // Debug.Log("Pressed");
            return true;
        }

        // move this somewhere else
        public void ThrowPhysicalItem(ItemThrowData data)
        {
            // physInteractions.ThrowPhysicalItem(data);
            
            var physItem = data.PhysItem;
            
            var rb = physItem.GetComponent<ForceReceiverMovementBehaviour>();
            
//             Debug.Log(data.ThrowForce);
            rb.AddForce(data.ThrowForce);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<IInteractable>() != null) Debug.Log("going to, is interactable", other);
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.GetComponent<IInteractable>() != null) Debug.Log("going away, but is interactable", other);
        }

        #if UNITY_EDITOR
        [ContextMenu("Test PhysicalItemFactory.CreateInstance")]
        private void CreateInstanceTest()
        {
            PhysicalItemFactory.CreateInstance(Vector3.zero, Quaternion.identity, null);
        }
        #endif
    }
}