using System;
using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class SlidingMovementBehaviour : MonoBehaviour
    {
        [SerializeField] private CharacterMovementMotor movementMotor = default;
        [SerializeField] private SlidingMovement slidingMovement = default;
        
        public SlidingMovement SlidingMovement => slidingMovement;

        private CharacterController chController;

        private void Awake()
        {
            if (movementMotor == null)
            {
                movementMotor = GetComponent<CharacterMovementMotor>();
            }

            chController = movementMotor.GetComponent<CharacterController>();
        }

        private void OnEnable() => movementMotor.AddModifier(slidingMovement);
        private void OnDisable() => movementMotor.RemoveModifier(slidingMovement);
        private void FixedUpdate() => slidingMovement.Tick(Time.fixedDeltaTime, transform.position + chController.center);

        public bool ShouldSlide() => slidingMovement.ShouldSlide(transform.position + chController.center, chController.slopeLimit);
    }
}
