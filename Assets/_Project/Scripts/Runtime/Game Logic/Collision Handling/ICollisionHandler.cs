using UnityEngine;

namespace WizardGame.CollisionHandling
{
	public interface ICollisionHandler
	{
		/// <summary>
		/// Guaranteed to be called ONLY if there was a gameobject hit, therefore other is never null
		/// </summary>
		/// <param name="other"></param>
		/// <param name="type"></param>
		void ProcessCollision(GameObject other, CollisionType type);
	}
}
