using System;
using UnityEngine;

namespace WizardGame.Stats_System
{
    [Serializable]
    public class StatTypeDependency
    {
        [SerializeField] private string name = default;
        [SerializeField] private StatType statDependingOn = default;
        [SerializeField] private float statMultiplier = default;
        
        public StatType StatDependingOn => statDependingOn;
        public float StatMultiplier => statMultiplier;

        public string Name
        {
            get => name;
            set => name = value;
        }
    }
}