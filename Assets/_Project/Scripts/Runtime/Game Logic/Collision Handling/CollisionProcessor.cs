using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.CollisionHandling
{
	public class CollisionProcessor : MonoBehaviour
	{
		private readonly List<ICollisionHandler> collisionHandlers = new List<ICollisionHandler>();

		private void Awake()
		{
			TryGetHandlersOnGameObject();
		}

		private void TryGetHandlersOnGameObject()
		{
			var handlers = GetComponents<ICollisionHandler>();

			foreach (var handler in handlers)
			{
				collisionHandlers.Add(handler);
			}
		}

		private void ProcessCollisions(GameObject other, CollisionType type)
		{
			foreach (var handler in collisionHandlers)
			{
				handler.ProcessCollision(other, type);
			}
		}

		public void AddCollisionHandler(ICollisionHandler handler)
		{
			if (collisionHandlers.Contains(handler)) return;

			collisionHandlers.Add(handler);
		}

		public bool RemoveCollisionHandler(ICollisionHandler handler)
		{
			return collisionHandlers.Remove(handler);
		}

		private bool TryGetGameObjectFromCollider(Collider other, out GameObject obj)
		{
			obj = null;

			if (other.attachedRigidbody)
			{
				obj = other.attachedRigidbody.gameObject;

				return true;
			}

			if (other.gameObject)
			{
				obj = other.gameObject;

				return true;
			}

			return false;
		}
		
		#region collision callbacks
		public void OnCollisionEnter(Collision other)
		{
			if (!TryGetGameObjectFromCollider(other.collider, out var hit)) return;

			ProcessCollisions(hit, CollisionType.CollisionEnter);
		}

		private void OnCollisionStay(Collision other)
		{
			if (!TryGetGameObjectFromCollider(other.collider, out var hit)) return;
			
			ProcessCollisions(hit, CollisionType.CollisionStay);
		}

		private void OnCollisionExit(Collision other)
		{
			if (!TryGetGameObjectFromCollider(other.collider, out var hit)) return;
			
			ProcessCollisions(hit, CollisionType.CollisionExit);
		}

		public void OnTriggerEnter(Collider other)
		{
			if (!TryGetGameObjectFromCollider(other, out var hit)) return;
			
			ProcessCollisions(hit, CollisionType.TriggerEnter);
		}

		private void OnTriggerStay(Collider other)
		{
			if (!TryGetGameObjectFromCollider(other, out var hit)) return;
			
			ProcessCollisions(hit, CollisionType.TriggerStay);
		}

		private void OnTriggerExit(Collider other)
		{
			if (!TryGetGameObjectFromCollider(other, out var hit)) return;
			
			ProcessCollisions(hit, CollisionType.TriggerExit);
		}
		#endregion
	}
}
