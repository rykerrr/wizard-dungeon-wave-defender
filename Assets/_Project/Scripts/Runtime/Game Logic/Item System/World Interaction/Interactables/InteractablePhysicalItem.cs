using System;
using System.Collections;
using System.Collections.Generic;
using WizardGame.Item_System.Items;
using UnityEngine;

namespace WizardGame.Item_System.World_Interaction
{
    public class InteractablePhysicalItem : MonoBehaviour, IInteractable
    {
        [Header("References")]
        [SerializeField] private PhysicaltemMovement physItemMovement = default;
        
        [Header("Preferences")]
        [SerializeField] private InventoryItem targetItem = default;
        [SerializeField] private string interactableDescription = default;
        
        public InventoryItem TargetItem => targetItem;
        public string InteractableDescription => $"Item: {TargetItem.Name}\n{interactableDescription}";

        public void Init(InventoryItem itemToInit)
        {
            targetItem = itemToInit;
        }
        
        private void OnEnable()
        {
            physItemMovement.EnablePhysics(true);
        }

        private void OnDisable()
        {
            physItemMovement.ForcePhysicsSet(false);
        }

        public void OnPlayerEnter()
        {
            throw new NotImplementedException();
        }

        public void OnPlayerExit()
        {
            throw new NotImplementedException();
        }
    }
}