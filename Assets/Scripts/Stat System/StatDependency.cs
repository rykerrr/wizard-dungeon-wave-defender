using System;

namespace WizardGame.Stats_System
{
    [Serializable]
    public class StatDependency
    {
        private StatBase statDependingOn;
        private float statMultiplier;

        public StatBase StatDependingOn => statDependingOn;
        public float StatMultiplier => statMultiplier;

        public StatDependency(StatBase statDependingOn, float statMultiplier)
        {
            this.statDependingOn = statDependingOn;
            this.statMultiplier = statMultiplier;
        }
    }
}