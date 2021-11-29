using System;
using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Stats_System
{
    [CreateAssetMenu(fileName = "New Dependant Stat Type", menuName = "Stats/Default Dependant Stat")]
    public class DependantStatType : StatType
    {
        [SerializeField] private List<StatTypeDependency> statsDependingOn = new List<StatTypeDependency>();
        
        [SerializeField] private int value = default;

        public List<StatTypeDependency> StatsDependingOn => statsDependingOn;

        public override int Value => value;

        protected override void OnValidateUtility()
        {
            try
            {
                foreach (var statTypeDep in statsDependingOn)
                {
                    statTypeDep.Name = statTypeDep.StatDependingOn.Name;
                }
            }
            catch (NullReferenceException) { }
        }

        public override string ToString()
        {
            return
                $"Default Dependant Stat Type Name: {Name} | Default Dependant Stat Type Value: {Value}\nValues depending on: {StatsDependingOn}";
        }
    }
}
