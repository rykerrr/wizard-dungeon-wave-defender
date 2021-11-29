using UnityEngine;

namespace WizardGame.CollisionHandling
{
	public class OnCollisionLogCollisionMessage : MonoBehaviour, ICollisionHandler
	{
		public void ProcessCollision(GameObject other, CollisionType type)
		{
			LogCollisionMessage(other);
		}

		private void LogCollisionMessage(GameObject other)
		{
#if UNITY_EDITOR
			Debug.Log($"Object {gameObject.name} hit {other.gameObject.name}");
#endif
		}
	}
}
