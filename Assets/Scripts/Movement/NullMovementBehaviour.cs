using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement
{
    public class NullMovementBehaviour : MonoBehaviour
    {
        [SerializeField] private CharacterMovementMotor movementMotor;
        [SerializeField] private NullMovement nullMovement;

        public NullMovement NullMovement => nullMovement;

        private void OnEnable() => movementMotor.AddModifier(nullMovement);
        private void OnDisable() => movementMotor.RemoveModifier(nullMovement);
        private void FixedUpdate() => nullMovement.Tick(Time.fixedDeltaTime);
    }
}
