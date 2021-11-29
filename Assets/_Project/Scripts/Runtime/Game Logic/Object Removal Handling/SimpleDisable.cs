using UnityEngine;

namespace WizardGame.ObjectRemovalHandling
{
	public class SimpleDisable : MonoBehaviour, IRemovalProcessor
	{
		public void Remove()
		{
			Destroy(gameObject);
		}
	}
}
