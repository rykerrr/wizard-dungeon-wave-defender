using System;
using UnityEngine;

namespace WizardGame.Movement.Rotation
{
    [Serializable]
    public class RotateObjectTowardsTarget
    {
        [SerializeField] private Transform objToRotate = default;
        [SerializeField] private Transform target = default;
        [SerializeField] private bool enableRotate = default;
        
        public Transform ObjectToRotate
        {
            get => objToRotate;
            set => objToRotate = value;
        }

        public Transform Target
        {
            get => target;
            set => target = value;
        }

        public bool EnableRotate
        {
            get => enableRotate;
            set => enableRotate = value;
        }
        
        // rotation mask keeps rotation in it's axis only, e.g 0,1,0 is rotation on y only
        public void Tick(Vector3 rotationMask)
        {
            if (!enableRotate) return;

            Vector3 forward = (target.position - objToRotate.position).normalized;

            Vector3 newLookRotation = Quaternion.LookRotation(forward, Vector3.up).eulerAngles;

            objToRotate.rotation = Quaternion.Euler(Vector3.Scale(newLookRotation, rotationMask));
        }
    }
}