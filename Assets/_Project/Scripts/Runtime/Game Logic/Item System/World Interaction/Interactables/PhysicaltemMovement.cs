using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Item_System.World_Interaction
{
    public class PhysicaltemMovement : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Rigidbody thisRb = default;
        
        [Header("Preferences")]
        [SerializeField] private List<MonoBehaviour> physComponents = default;
        [SerializeField] private float secondsForPhysActivation = default;

        private WaitForSeconds waitForPhysActivation = null;
        
        public void ForcePhysicsSet(bool setEnabled)
        {
            physComponents.ForEach(x => x.enabled = setEnabled);
            thisRb.isKinematic = setEnabled;
        }
        
        public void EnablePhysics(bool enable)
        {
            StartCoroutine(EnablePhysicsCoroutine(enable));
        }
        
        private IEnumerator EnablePhysicsCoroutine(bool enable)
        {
            waitForPhysActivation ??= new WaitForSeconds(secondsForPhysActivation);
            
            yield return waitForPhysActivation;
            
            ForcePhysicsSet(enable);
        }
    }
}