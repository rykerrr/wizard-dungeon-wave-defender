﻿using WizardGame.Item_System.World_Interaction;
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
                rotatePhysItemAroundAxis.Tick(Vector3.one, 1f);
            }
            else
            {
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