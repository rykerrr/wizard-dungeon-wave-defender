using System;

namespace WizardGame.Combat_System.Cooldown_System
{
    public interface IHasCooldown
    {
        Guid Id { get; }
        float CooldownDuration { get; }
    }
}