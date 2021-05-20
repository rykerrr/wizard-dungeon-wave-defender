using System;
using UnityEngine;

namespace WizardGame.Movement.Rotation
{
    [Serializable]
    public class RotateObjectAroundAxis
    {
        [SerializeField] private Transform objToRotate = default;
        [SerializeField] private bool enableRotate = default;

        public Transform ObjToRotate
        {
            get => objToRotate;
            set => objToRotate = value;
        }

        public bool EnableRotate
        {
            get => enableRotate;
            set => enableRotate = value;
        }

        public void Tick(Vector3 axis, float value)
        {
            if (!enableRotate) return;
            
            ObjToRotate.Rotate(axis, value);
        }
    }
}