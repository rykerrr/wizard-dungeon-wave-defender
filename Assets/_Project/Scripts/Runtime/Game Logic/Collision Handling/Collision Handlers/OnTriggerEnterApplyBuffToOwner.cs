using UnityEngine;
using WizardGame.Stats_System;

namespace WizardGame.CollisionHandling
{
	public class OnTriggerEnterApplyBuffToOwner : MonoBehaviour, ICollisionHandler
	{
		private StatType statKey;
		private StatModifier statModifier;
		private GameObject owner;

		public void Init(StatType statKey, StatModifier statModifier, GameObject owner)
		{
			this.statKey = statKey;
			this.statModifier = statModifier;
			this.owner = owner;

			statModifier.Source = gameObject;
		}
		
		public void ProcessCollision(GameObject other, CollisionType type)
		{
			if (type != CollisionType.TriggerEnter) return;

			var hasStats = other.TryGetComponent<StatsSystemBehaviour>(out var statsSys);
			if (!hasStats) return;
			
			ApplyBuff(statsSys);
		}
		
		private void ApplyBuff(params StatsSystemBehaviour[] targets)
		{
			Debug.Log("Applying buff...");
			
			for (var i = targets.Length - 1; i >= 0; i--)
			{
				targets[i].StatsSystem.AddModifierTo(statKey, statModifier);
			}
		}
	}
}
