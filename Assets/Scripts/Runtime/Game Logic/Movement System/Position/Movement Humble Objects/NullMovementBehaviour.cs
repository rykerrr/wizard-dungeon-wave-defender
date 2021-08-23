using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class NullMovementBehaviour : MovementModifierBehaviour
    {
        [SerializeField] private NullMovement nullMovement = default;

        private NullMovement NullMovement => nullMovement;

        public override float ExternalMult
        {
            get => NullMovement.ExternalMult;
            set => NullMovement.ExternalMult = value;
        }
        
        protected override void OnEnable() => movementMotor.AddModifier(NullMovement);
        protected override void OnDisable() => movementMotor.RemoveModifier(NullMovement);
        protected override void FixedUpdate() => NullMovement.Tick(Time.fixedDeltaTime);
    }
}
