using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class GravityMovementBehaviour : MovementModifierBehaviour
    {
        [SerializeField] private GravityMovement gravityMovement;
        
        private GravityMovement GravityMovement => gravityMovement;

        public override float ExternalMult
        {
            get => GravityMovement.ExternalMult;
            set => GravityMovement.ExternalMult = value;
        }
        
        protected override void Awake()
        {
            base.Awake();
        }

        protected override  void OnEnable() => movementMotor.AddModifier(GravityMovement);
        protected override  void OnDisable() => movementMotor.RemoveModifier(GravityMovement);
        protected override  void FixedUpdate() => GravityMovement.Tick(Time.fixedDeltaTime, chController.isGrounded);
    }
}
