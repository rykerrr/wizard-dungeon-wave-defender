using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class ForceReceiverMovementBehaviour : MovementModifierBehaviour
    {
        [SerializeField] private ForceReceiverMovement forceReceiver;
        
        public ForceReceiverMovement ForceReceiver => forceReceiver;
        
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnEnable() => movementMotor.AddModifier(forceReceiver);
        protected override void OnDisable() => movementMotor.RemoveModifier(forceReceiver);
        protected override void FixedUpdate() => forceReceiver.Tick(Time.fixedDeltaTime);

        public void AddForce(Vector3 force) => forceReceiver.AddForce(force);
    }
}
