namespace WizardGame.Health_System
{
    public interface IHealth : IHealable, IDamageable
    {
        public int CurrentHealth { get; }
        public int MaxHealth { get; }
    }
}