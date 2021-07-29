using System;
using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Stats_System
{
    [CreateAssetMenu(fileName = "Base Dependant Stat Type", menuName = "Stats/Default Dependant Stat")]
    public class DependantStatType : StatType
    {
        [SerializeField] private new string name = default;
        [SerializeField] private List<StatTypeDependency> statsDependingOn = new List<StatTypeDependency>();
        
        [SerializeField] private int value = default;

        public List<StatTypeDependency> StatsDependingOn => statsDependingOn;

        public override string Name
        {
            get => name;
            protected set => name = value;
        }
        public override int Value => value;

        protected override void OnValidate()
        {
            base.OnValidate();
            
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
