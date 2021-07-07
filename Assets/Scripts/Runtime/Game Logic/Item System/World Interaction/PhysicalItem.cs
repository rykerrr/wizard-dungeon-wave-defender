using System;
using System.Collections;
using System.Collections.Generic;
using WizardGame.Item_System.Items;
using UnityEngine;

namespace WizardGame.ItemSystem.World_Interaction
{
    public class PhysicalItem : MonoBehaviour
    {
        [SerializeField] private PhysicalItemDisplay physItemDisplay;
        [SerializeField] private List<MonoBehaviour> physComponents;
        
        [SerializeField] private float secondsForPhysActivation;

        private Rigidbody thisRb = default;
        private WaitForSeconds waitForPhysActivation = default;

        public HotbarItem TargetItem => physItemDisplay.TargetItem;

        private void Awake()
        {
            thisRb = GetComponent<Rigidbody>();
            waitForPhysActivation = new WaitForSeconds(secondsForPhysActivation);
        }

        private IEnumerator EnablePhysicsCoroutine(bool enable)
        {
            yield return waitForPhysActivation;
            
            SetPhysicsComponentsEnabled(enable);
        }
        
        private void EnablePhysics(bool enable)
        {
            StartCoroutine(EnablePhysicsCoroutine(enable));
        }

        public void InitDisplay(InventoryItem itemToinit)
        {
            physItemDisplay.Init(itemToinit);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerInteractionBehavior>()) return;
            
            physItemDisplay.OnPlayerEnter();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.GetComponent<PlayerInteractionBehavior>()) return;
            
            physItemDisplay.OnPlayerExit();
        }

        private void SetPhysicsComponentsEnabled(bool enabled)
        {
            physComponents.ForEach(x => x.enabled = enabled);
            thisRb.isKinematic = enabled;
        }

        private void OnEnable()
        {
            EnablePhysics(true);
        }

        private void OnDisable()
        {
            SetPhysicsComponentsEnabled(false);
        }
    }
}