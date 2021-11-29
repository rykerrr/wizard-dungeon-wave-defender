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
			
			statModifier.Source = gameObject;
		}
		
		public void ProcessCollision(GameObject other, CollisionType type)
		{
			if (type != CollisionType.TriggerExit || other != owner) return;
			
			var hasStats = other.TryGetComponent<StatsSystemBehaviour>(out var statsSys);
			if (!hasStats) return;
			
			statsSys.StatsSystem.RemoveModifiersFromSource(statKey, statsSys);
		}
		
		private void RemoveBuff(params StatsSystemBehaviour[] targets)
		{
			for (var i = targets.Length - 1; i >= 0; i--)
			{
				var removeCount = targets[i].StatsSystem.RemoveModifiersFromSource(statKey, gameObject);
				
				Debug.Log($"Stat Modifiers removed from object {targets[i].gameObject.name}: {removeCount}");
			}
		}
		
		public void OnDisable()
		{
			if (!owner) return;
			
			RemoveBuff(owner.GetComponent<StatsSystemBehaviour>());
		}
	}
}
