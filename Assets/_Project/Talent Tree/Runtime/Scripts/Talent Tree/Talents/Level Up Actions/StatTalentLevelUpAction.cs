using System;
using UnityEngine;
using WizardGame.Stats_System;

namespace Talent_Tree
{
    [CreateAssetMenu(menuName = "Talents/New Talent Stat Levelup Action", fileName = "New Talent Stat Levelup Action")]
    public class StatTalentLevelUpAction : TalentLevelUpAction
    {
        [SerializeField] private StatType key = default;
        [SerializeField] private StatModifier modifier = default;
        
        public StatsSystem StatsSystem { get; set; }
        
        public override void LevelUp()
        {
            var missingRef = ReferenceEquals(StatsSystem, null);
            if (missingRef)
            {
                Debug.LogError("Missing StatsSystem Reference", this);

                return;
            }
            
            StatsSystem.AddModifierTo(key, new StatModifier(modifier.Type, modifier.Value, this));
        }
    }
}