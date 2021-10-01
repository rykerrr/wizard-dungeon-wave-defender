using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public class ForceReceiverMovementBehaviour : MovementModifierBehaviour
    {
        [SerializeField] private ForceReceiverMovement forceReceiver;

        private ForceReceiverMovement ForceReceiver => forceReceiver;

        public override float ExternalMult
        {
            get => ForceReceiver.ExternalMult;
            set => ForceReceiver.ExternalMult = value;
        }

        protected override void OnEnable() => movementMotor.AddModifier(ForceReceiver);
        protected override void OnDisable() => movementMotor.RemoveModifier(ForceReceiver);
        protected override void FixedUpdate() => ForceReceiver.Tick(Time.fixedDeltaTime);

        public void AddForce(Vector3 force) => ForceReceiver.AddForce(force);
    }
}
