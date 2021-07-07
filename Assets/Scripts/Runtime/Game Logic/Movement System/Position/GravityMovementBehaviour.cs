using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class GravityMovementBehaviour : MonoBehaviour
    {
        [SerializeField] private CharacterMovementMotor movementMotor;
        [SerializeField] private GravityMovement gravityMovement;
        
        public GravityMovement GravityMovement => gravityMovement;

        private CharacterController chController;

        private void Awake()
        {
            if (movementMotor == null)
            {
                movementMotor = GetComponent<CharacterMovementMotor>();
            }
            
            chController = GetComponent<CharacterController>();
        }

        private void OnEnable() => movementMotor.AddModifier(gravityMovement);
        private void OnDisable() => movementMotor.RemoveModifier(gravityMovement);
        private void FixedUpdate() => gravityMovement.Tick(Time.fixedDeltaTime, chController.isGrounded);
    }
}
