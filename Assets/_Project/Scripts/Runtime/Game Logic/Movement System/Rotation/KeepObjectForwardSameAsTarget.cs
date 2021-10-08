using System;
using UnityEngine;

namespace WizardGame.Movement.Rotation
{
    [Serializable]
    public class KeepObjectForwardSameAsTarget
    {
        [SerializeField] private bool enabledFollow = false;
        [SerializeField] private Transform forwardTarget = default;

        public Transform ForwardTarget
        {
            get => forwardTarget;
            set => forwardTarget = value;
        }

        public Vector3 Tick()
        {
            return forwardTarget.forward;
        }
    }
}