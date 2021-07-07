using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Stats_System;

namespace WizardGame.Stats_System
{
    [CreateAssetMenu(fileName = "New Entity Stats", menuName = "Stats/Entity Stats")]
    public class EntityStats : ScriptableObject
    {
        [SerializeField] private float lifeValueExpMultiplier = 1;
        [SerializeField] private List<EntityStat> defaultEntityStats = new List<EntityStat>();

        public float LifeValueExpMultiplier => lifeValueExpMultiplier;

        public List<EntityStat> DefaultEntityStats
        {
             get
             {
                 defaultEntityStats.Sort(ReturnEntityStatByBaseStatFirst);
 
                 return defaultEntityStats;
             }
         }
 
         private int firstDepStatIndex = default;
 
         public int FirstDepStatIndex
         {
             get
             {
                 firstDepStatIndex = defaultEntityStats.FindIndex(x => x.StatType.GetType() == typeof(DependantStatType));
 
                 return firstDepStatIndex;
             }
         }
 
         private void OnValidate()
         {
             try
             {
                 foreach (var stat in defaultEntityStats)
                 {
                     stat.Name = stat.StatType.Name;
                 }
             }
             catch (NullReferenceException) { }
         }
 
         private int ReturnBaseStatFirst(StatType x, StatType y)
         {
             var a = x.GetType() == typeof(DependantStatType) ? 1 : -1;
             var b = y.GetType() == typeof(DependantStatType) ? 1 : -1;
             
             return a - b;
         }
         
         private int ReturnEntityStatByBaseStatFirst(EntityStat x, EntityStat y)
         {
             var a = x.StatType.GetType() == typeof(DependantStatType) ? 1 : -1;
             var b = y.StatType.GetType() == typeof(DependantStatType) ? 1 : -1;
             
             return a - b;
         }
    }
}