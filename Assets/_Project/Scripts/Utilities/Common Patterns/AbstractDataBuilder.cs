using UnityEngine;

namespace WizardGame.Utility.Patterns
{
    public abstract class AbstractDataBuilder<T>
    {
        public abstract T Build();

        public static implicit operator T(AbstractDataBuilder<T> builder)
        {
            return builder.Build();
        }
    }
}
