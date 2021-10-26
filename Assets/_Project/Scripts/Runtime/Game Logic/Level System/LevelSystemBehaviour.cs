using System.Collections.Generic;
using UnityEngine;
using WizardGame.Health_System;
using WizardGame.Stats_System;

namespace WizardGame.Level_System
{
    public class LevelSystemBehaviour : MonoBehaviour
    {
        [SerializeField] private StatsSystemBehaviour statsSystem = default;
        [SerializeField] private List<StatType> statTypesToGrow = new List<StatType>();
        [SerializeField] private int startLevel = 30;
        
        private LevelSystem levelSystem = default;

        public LevelSystem LevelSystem => levelSystem ??= new LevelSystem(
            0);

        private void Awake()
        {
            // LevelSystem.Init(0, statsSystem.StatsSystem, GetRequiredStatsFromStatTypes());

            var healthSysBehaviour = GetComponent<HealthSystemBehaviour>();

            LevelSystem.onLevelUpEvent += () =>
            {
                foreach (var stat in GetRequiredStatsFromStatTypes())
                {
                    stat.GrowByGrowthRate();
                }
            };
            
            healthSysBehaviour.DeathProcessor.onDeathEvent += source =>
            {
                // This code runs on the object that gets killed
                
                Debug.Log($"Murderer! {gameObject} was murdered by {source}");
                
                LevelSystemBehaviour sourceLvSysBehaviour = default;

                if (!ReferenceEquals(sourceLvSysBehaviour = source.GetComponent<LevelSystemBehaviour>(), null))
                {
                    var sourceLvSystem = sourceLvSysBehaviour.LevelSystem;
                    var lifeExpValue = LevelSystem.CalculateLifeExperienceValue(LevelSystem.CurLevel,
                        statsSystem.Entity.LifeValueExpMultiplier);
                    
                    Debug.Log($"Value of {gameObject.name}'s life in experience points: {lifeExpValue}");
                    
                    sourceLvSystem.AddExp(lifeExpValue);
                }
            };
            
            LevelSystem.AddExp(LevelSystem.CalculateTotalExpRequiredForLevel(startLevel));
        }

        private List<StatBase> GetRequiredStatsFromStatTypes()
        {
            List<StatBase> statsToGrow = new List<StatBase>();
            var statsSys = statsSystem.StatsSystem;
            
            foreach (var statType in statTypesToGrow)
            {
                statsToGrow.Add(statsSys.GetStat(statType));
            }

            return statsToGrow;
        }

        [Space(10), Header("Debug"), Space(10)]
        #region Debug
        [SerializeField] private int expToGive = default;

        [ContextMenu("Add exp, attempt to level up and grow stats")]
        private void GrowGivenStat()
        {
            LevelSystem.AddExp(expToGive);
        }
        
        [ContextMenu("Debug dump LevelSystem data")]
        private void DumpLevelSystemData()
        {   
            Debug.Log(LevelSystem.ToString());
        }
        #endregion
    }
}