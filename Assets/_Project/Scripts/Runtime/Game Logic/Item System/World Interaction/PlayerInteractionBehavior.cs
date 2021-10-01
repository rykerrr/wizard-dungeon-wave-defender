using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.CustomEventSystem;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Item_System.World_Interaction.UI;
using WizardGame.Movement.Position;

namespace WizardGame.Item_System.World_Interaction
{
    public class PlayerInteractionBehavior : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private InteractButtonUI interactButton;
        
        [Header("Preferences")]
        [SerializeField] private Inventory inventory = default;
        [SerializeField] private GetClosestInteractable getClosestInteractable = default;

        private readonly List<IInteraction> interactions = new List<IInteraction>();
        private IInteractable nearestInteractable = default;
        
        private StringBuilder sb;

        private void Awake()
        {
            InitInteractions();

            sb = new StringBuilder();
        }

        private void InitInteractions()
        {
            interactions.Add(new PhysicalItemInteractions(inventory.ItemContainer));
            interactions.Add(new UIStandInteractions());
        }

        private void Update()
        {
            if (GetNearestInteractable()) return;

            var input = InputMethod();
            if (!input) return;
                
            TryProcessInteraction();
        }

        private bool GetNearestInteractable()
        {
            var hits = getClosestInteractable.FindInteractables(transform.position, transform.forward);

            if (hits == null)
            {
                interactButton.TryDisable();

                return true;
            }

            nearestInteractable = hits[0].gameObject.GetComponent<IInteractable>();
            if (ReferenceEquals(nearestInteractable, null)) return false;

            interactButton.UpdateUI(nearestInteractable.InteractUseDescription);

            return false;
        }

        public void TryProcessInteraction()
        {
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
            var physItem = data.PhysItem;
            
            var rb = physItem.GetComponent<ForceReceiverMovementBehaviour>();
            
            rb.AddForce(data.ThrowForce);
        }
    }
}