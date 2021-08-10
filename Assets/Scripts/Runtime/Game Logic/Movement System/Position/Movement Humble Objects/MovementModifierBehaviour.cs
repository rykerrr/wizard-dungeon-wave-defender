using System;
using UnityEngine;

namespace WizardGame.Movement.Position
{
    public abstract class MovementModifierBehaviour : MonoBehaviour
    {
        [SerializeField] protected CharacterMovementMotor movementMotor = default;

        protected CharacterController chController = default;
        
        protected virtual void Awake()
        {
            movementMotor ??= GetComponent<CharacterMovementMotor>();
            chController ??= movementMotor.GetComponent<CharacterController>();
        }
        

        protected abstract void OnEnable();
        protected abstract void OnDisable();

        protected abstract void FixedUpdate();
    }
}