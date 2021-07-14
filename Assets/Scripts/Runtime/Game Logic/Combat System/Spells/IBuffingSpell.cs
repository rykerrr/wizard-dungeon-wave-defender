using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public interface IBuffingSpell
    {
        void ApplyBuff(params StatsSystemBehaviour[] targets);
        void RemoveBuff(params StatsSystemBehaviour[] targets);
    }
}