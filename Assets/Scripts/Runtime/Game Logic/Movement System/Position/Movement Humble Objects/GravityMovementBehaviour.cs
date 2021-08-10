using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class GravityMovementBehaviour : MovementModifierBehaviour
    {
        [SerializeField] private GravityMovement gravityMovement;
        
        public GravityMovement GravityMovement => gravityMovement;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override  void OnEnable() => movementMotor.AddModifier(gravityMovement);
        protected override  void OnDisable() => movementMotor.RemoveModifier(gravityMovement);
        protected override  void FixedUpdate() => gravityMovement.Tick(Time.fixedDeltaTime, chController.isGrounded);
    }
}
