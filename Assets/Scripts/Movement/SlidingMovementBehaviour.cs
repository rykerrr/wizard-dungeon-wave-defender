using System;
using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement
{
    public class SlidingMovementBehaviour : MonoBehaviour
    {
        [SerializeField] private CharacterMovementMotor movementMotor;
        [SerializeField] private SlidingMovement slidingMovement;

        [SerializeField] private float slideSpeed;
        
        public SlidingMovement SlidingMovement => slidingMovement;

        private CharacterController chController;

        private void Awake()
        {
            if (movementMotor == null)
            {
                movementMotor = GetComponent<CharacterMovementMotor>();
                chController = GetComponent<CharacterController>();
            }
            
            slidingMovement.Init(chController.height * 0.75f + chController.radius);
        }

        private void OnEnable() => movementMotor.AddModifier(slidingMovement);
        private void OnDisable() => movementMotor.RemoveModifier(slidingMovement);
        private void FixedUpdate() => slidingMovement.Tick(Time.fixedDeltaTime, slideSpeed, transform.position + chController.center);

        public bool ShouldSlide() => slidingMovement.ShouldSlide(transform.position + chController.center, chController.slopeLimit);
    }
}
