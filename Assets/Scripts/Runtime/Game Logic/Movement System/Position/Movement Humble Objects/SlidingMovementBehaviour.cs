using System;
using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class SlidingMovementBehaviour : MovementModifierBehaviour
    {
        [SerializeField] private SlidingMovement slidingMovement = default;
        
        private SlidingMovement SlidingMovement => slidingMovement;

        public override float ExternalMult
        {
            get => SlidingMovement.ExternalMult;
            set => SlidingMovement.ExternalMult = value;
        }
        
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnEnable() => movementMotor.AddModifier(SlidingMovement);
        protected override void OnDisable() => movementMotor.RemoveModifier(SlidingMovement);
        protected override void FixedUpdate() => SlidingMovement.Tick(Time.fixedDeltaTime, transform.position + chController.center);

        public bool ShouldSlide() => SlidingMovement.ShouldSlide(transform.position + chController.center, chController.slopeLimit);
    }
}
