using UnityEngine;

namespace WizardGame.Stats_System
{
    public abstract class StatType : ScriptableObject
    {
        public abstract string Name { get; }
        public abstract int Value { get; }

        public abstract override string ToString();
    }
}