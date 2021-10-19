using UnityEngine;

namespace WizardGame.Movement.Rotation.ForwardModifiers
{
	public class SetObjectForward : MonoBehaviour
	{
		[SerializeField] private Transform targetToSet = default;

		private IForwardModifier curSelectedModifier = default;
		
		public Vector3 NextForwardVector { get; private set; } = Vector3.zero;

		private void Update()
		{
			UpdateFowVector();
		}

		private void UpdateFowVector()
		{
			if (NextForwardVector.Equals(Vector3.zero)) return;
			
			targetToSet.forward = NextForwardVector;
		}

		public void SetSelectedModifier(IForwardModifier modifier)
		{
			curSelectedModifier = modifier;
		}

		public void TrySetNextForwardVector(IForwardModifier modifier)
		{
			if (modifier != curSelectedModifier) return;

			NextForwardVector = modifier.Value;
		}
	}
}
