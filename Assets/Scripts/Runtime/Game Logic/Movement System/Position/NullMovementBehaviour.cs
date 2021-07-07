using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class NullMovementBehaviour : MonoBehaviour
    {
        [SerializeField] private CharacterMovementMotor movementMotor = default;
        [SerializeField] private NullMovement nullMovement = default;

        public NullMovement NullMovement => nullMovement;

        private void OnEnable() => movementMotor.AddModifier(nullMovement);
        private void OnDisable() => movementMotor.RemoveModifier(nullMovement);
        private void FixedUpdate() => nullMovement.Tick(Time.fixedDeltaTime);
    }
}
