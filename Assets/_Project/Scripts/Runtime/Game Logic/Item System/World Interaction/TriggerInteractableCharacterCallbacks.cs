using UnityEngine;

namespace WizardGame.Item_System.World_Interaction
{
	public class TriggerInteractableCharacterCallbacks : MonoBehaviour
	{
		private void OnTriggerEnter(Collider other)
		{
			IInteractable interactable;
			if (ReferenceEquals(interactable = other.GetComponent<IInteractable>(), null)) return;

			interactable.OnCharacterEnter(transform);
		}

		private void OnTriggerExit(Collider other)
		{
			IInteractable interactable;
			if (ReferenceEquals(interactable = other.GetComponent<IInteractable>(), null)) return;

			interactable.OnCharacterExit(transform);
		}
	}
}
