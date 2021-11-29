using WizardGame.Utility;

namespace WizardGame.Stats_System
{
    public abstract class StatType : ScriptableObjectAutoNameSet
    {
        public abstract int Value { get; }

        public abstract override string ToString();
    }
}