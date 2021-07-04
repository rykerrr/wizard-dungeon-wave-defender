using System.Collections.Generic;
using UnityEngine;
using WizardGame.Stats_System;

namespace WizardGame.Stats_System
{
    [CreateAssetMenu(fileName = "New Entity Stats", menuName = "Stats/Entity Stats")]
    public class EntityStats : ScriptableObject
    {
        [SerializeField] private List<StatType> defaultStatTypes = new List<StatType>();

        public List<StatType> DefaultStatTypes
        {
            get
            {
                // Keep this despite the fact it's sorting a SO?
                defaultStatTypes.Sort(ReturnBaseStatFirst);

                return defaultStatTypes;
            }
        }

        public int FirstDepStatIndex
        {
            get
            {
                firstDepStatIndex = defaultStatTypes.FindIndex(x => x.GetType() == typeof(DependantStatType));

                return firstDepStatIndex;
            }
        }

        private int firstDepStatIndex = default;
        
        private int ReturnBaseStatFirst(StatType x, StatType y)
        {
            var a = x.GetType() == typeof(DependantStatType) ? 1 : -1;
            var b = y.GetType() == typeof(DependantStatType) ? 1 : -1;
            
            return a - b;
        }
    }
}