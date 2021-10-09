using UnityEngine;
using WizardGame.Movement.Position;

namespace WizardGame.Movement.Rotation.ForwardModifiers
{
	public class SetObjectForwardTowardsMovementVector : MonoBehaviour, IForwardModifier
	{
		[SerializeField] private SetObjectForward forwSetter = default;
		[SerializeField] private CharacterMovementMotor motor = default;

		private Vector3 prevMovement = Vector3.zero;

		public Vector3 Value => prevMovement;
		
		private void Update()
		{
			var nextMovement = motor.LastFrameMovement;

			if (prevMovement == nextMovement) return;
			prevMovement = nextMovement;
			forwSetter.TrySetNextForwardVector(this);
		}
	}
}
