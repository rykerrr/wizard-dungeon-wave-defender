using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement
{
    public class ForceReceiverMovementBehaviour : MonoBehaviour
    {
        [SerializeField] private CharacterMovementMotor movementMotor;
        [SerializeField] private ForceReceiverMovement forceReceiver;

        [SerializeField] private float drag = 3f;
        
        public ForceReceiverMovement ForceReceiver => forceReceiver;

        private CharacterController chController;

        private void Awake()
        {
            if (movementMotor == null)
            {
                movementMotor = GetComponent<CharacterMovementMotor>();
                chController = GetComponent<CharacterController>();
            }
        }

        private void OnEnable() => movementMotor.AddModifier(forceReceiver);
        private void OnDisable() => movementMotor.RemoveModifier(forceReceiver);
        private void FixedUpdate() => forceReceiver.Tick(Time.fixedDeltaTime, drag);

        public void AddForce(Vector3 force) => forceReceiver.AddForce(force);
    }
}
