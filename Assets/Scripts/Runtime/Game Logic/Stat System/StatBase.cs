﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Stats_System
{
    public abstract class StatBase
    {
           protected Action statWasModified = delegate { };

        protected List<StatModifier> statModifiers = new List<StatModifier>();
        
        protected int baseValue = default;
        protected float growthRate = default;
        protected int actualValue = default;
        protected bool isDirty = true;
        
        public IReadOnlyList<StatModifier> StatModifiers => statModifiers.AsReadOnly();
        
        public Action StatWasModified
        {
            get => statWasModified;
            set => statWasModified = value;
        }
        
        public int BaseValue
        {
            get => baseValue;
            protected set
            {
                baseValue = value;

                StatWasModified.Invoke();
                isDirty = true;
            }
        }

        public float GrowthRate => growthRate;

        public int ActualValue
        {
            get
            {
                if (isDirty)
                {
                    actualValue = CalculateValue();
                    isDirty = false;
                }

                return actualValue;
            }
        }

        public bool IsDirty => isDirty;
        protected int OriginalValue { get; }


        public StatBase(StatType defType, float growthRate)
        {
            baseValue = defType.Value;
            OriginalValue = defType.Value;
            
            this.growthRate = growthRate;
        }

        protected int ApplyModifiers(int valueToApplyTo)
        {
            actualValue = valueToApplyTo;
            float additiveModSum = 0;

            statModifiers.Sort(StatModifier.CompareModifierOrder);

            for (int i = 0; i < StatModifiers.Count; i++)
            {
                var mod = StatModifiers[i];

                switch (mod.Type)
                {
                    case ModifierType.Flat:
                    {
                        actualValue += (int) Math.Round(mod.Value);
                        break;
                    }
                    case ModifierType.PercentAdditive:
                    {
                        additiveModSum += mod.Value;

                        if (i == StatModifiers.Count - 1 || StatModifiers[i + 1].Type != ModifierType.PercentAdditive)
                        {
                            actualValue = (int) Mathf.Round(actualValue * (1 + additiveModSum));
                        }

                        break;
                    }
                    case ModifierType.PercentFinalMultiplicative:
                    {
                        actualValue = (int) Math.Round(actualValue * (1 + mod.Value));

                        break;
                    }
                }
            }

            return actualValue;
        }

        public abstract int CalculateValue();

        public void GrowByGrowthRate()
        {
            var rand = new System.Random();
            var randMult = (float)rand.NextDouble();
            
            BaseValue += Mathf.RoundToInt(OriginalValue * GrowthRate + OriginalValue * randMult);
        }
        
        public void AddModifier(StatModifier mod)
        {
            isDirty = true;
            StatWasModified.Invoke();

            statModifiers.Add(mod);
        }

        public bool RemoveModifier(StatModifier mod)
        {
            isDirty = true;
            StatWasModified.Invoke();

            return statModifiers.Remove(mod);
        }

        public int RemoveModifiersFromSource(object source)
            => statModifiers.RemoveAll(x => x.Source == source);
    }
}