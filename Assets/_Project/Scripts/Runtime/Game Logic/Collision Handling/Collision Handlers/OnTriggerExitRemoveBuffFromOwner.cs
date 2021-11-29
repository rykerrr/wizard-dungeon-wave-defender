using UnityEngine;
using WizardGame.Stats_System;

namespace WizardGame.CollisionHandling
{
	public class OnTriggerExitRemoveBuffFromOwner : MonoBehaviour, ICollisionHandler
	{
		private StatType statKey;
		private StatModifier statModifier;
		private GameObject owner;

		public void Init(StatType statKey, StatModifier statModifier, GameObject owner)
		{
			this.statKey = statKey;
			this.statModifier = statModifier;
			this.owner = owner;
		}
		
		public void ProcessCollision(GameObject other, CollisionType type)
		{
			if (type != CollisionType.TriggerExit || other != owner) return;
			
			var hasStats = other.TryGetComponent<StatsSystemBehaviour>(out var statsSys);
			if (!hasStats) return;
			
			RemoveBuff(statsSys);
		}
		
		private void RemoveBuff(params StatsSystemBehaviour[] targets)
		{
			for (var i = targets.Length - 1; i >= 0; i--)
			{
				Debug.Log(targets.Length + " | " + i);
				Debug.Log(targets[i]);
				Debug.Log(owner);

				targets[i].StatsSystem.RemoveModifierFrom(statKey, statModifier);
			}
		}
		
		public void OnDisable()
		{
			if (!owner) return;
			
			RemoveBuff(owner.GetComponent<StatsSystemBehaviour>());
		}
	}
}
