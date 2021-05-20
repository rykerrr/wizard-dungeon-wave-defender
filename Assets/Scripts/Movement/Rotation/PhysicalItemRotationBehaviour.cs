using System;
using System.Runtime.CompilerServices;
using ItemSystem.World_Interaction;
using UnityEngine;

namespace WizardGame.Movement.Rotation
{
    public class PhysicalItemRotationBehaviour : MonoBehaviour
    {
        [SerializeField] private RotateObjectAroundAxis rotatePhysItemAroundAxis = default;
        [SerializeField] private RotateObjectTowardsTarget rotatePhysItemTowardsTarg = default;
        [SerializeField] private RotateObjectTowardsTarget rotateBillboardTowardsTarg = default;

        private Transform actualTarget = default;

        private void Update()
        {
            if (ReferenceEquals(actualTarget, null))
            {
                Debug.Log("rotating around axis");
                
                rotatePhysItemAroundAxis.Tick(Vector3.one, 1f);
            }
            else
            {
                Debug.Log("rotating towards obj " + actualTarget);
                
                rotatePhysItemTowardsTarg.Tick(Vector3.one);
                rotateBillboardTowardsTarg.Tick(new Vector3(0, 1, 0));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerInteractionBehavior>()) return;

            actualTarget = other.transform;
            
            rotatePhysItemTowardsTarg.Target = actualTarget;
            rotateBillboardTowardsTarg.Target = actualTarget;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.GetComponent<PlayerInteractionBehavior>()) return;
            
            actualTarget = null;
        }
    }
}