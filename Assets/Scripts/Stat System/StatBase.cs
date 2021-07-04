using System;
using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Stats_System
{
    public abstract class StatBase
    {
        protected List<StatModifier> modifiers = new List<StatModifier>();
        protected int baseValue = default;

        public IReadOnlyList<StatModifier> Modifiers => modifiers.AsReadOnly();

        protected Action statWasModified = delegate { };

        public Action StatWasModified
        {
            get => statWasModified;
            set => statWasModified = value;
        }

        public int BaseValue
        {
            get => baseValue;
            set
            {
                baseValue = value;

                StatWasModified.Invoke();
                isDirty = true;
            }
        }

        protected int actualValue = default;
        protected bool isDirty = true;

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

        public StatBase(StatType defType)
        {
            baseValue = defType.Value;
        }

        protected int ApplyModifiers(int valueToApplyTo)
        {
            actualValue = valueToApplyTo;
            float additiveModSum = 0;

            modifiers.Sort(StatModifier.CompareModifierOrder);

            for (int i = 0; i < Modifiers.Count; i++)
            {
                var mod = Modifiers[i];

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

                        if (i == Modifiers.Count - 1 || Modifiers[i + 1].Type != ModifierType.PercentAdditive)
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

        public void AddModifier(StatModifier mod)
        {
            isDirty = true;
            StatWasModified.Invoke();

            modifiers.Add(mod);
        }

        public bool RemoveModifier(StatModifier mod)
        {
            isDirty = true;
            StatWasModified.Invoke();

            return modifiers.Remove(mod);
        }

        public int RemoveModifiersFromSource(object source)
            => modifiers.RemoveAll(x => x.Source == source);
    }
}