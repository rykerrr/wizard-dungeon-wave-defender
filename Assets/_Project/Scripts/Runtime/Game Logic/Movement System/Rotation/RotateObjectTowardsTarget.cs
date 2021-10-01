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
        
        public void Tick()
        {
            if (!enableRotate) return;

            var relative = target.position - objToRotate.position;

            var newLookRotation = Quaternion.LookRotation(relative);

            objToRotate.rotation = newLookRotation;
        }
    }
}