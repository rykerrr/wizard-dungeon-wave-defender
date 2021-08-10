using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class NullMovementBehaviour : MovementModifierBehaviour
    {
        [SerializeField] private NullMovement nullMovement = default;

        public NullMovement NullMovement => nullMovement;

        protected override void OnEnable() => movementMotor.AddModifier(nullMovement);
        protected override void OnDisable() => movementMotor.RemoveModifier(nullMovement);
        protected override void FixedUpdate() => nullMovement.Tick(Time.fixedDeltaTime);
    }
}
