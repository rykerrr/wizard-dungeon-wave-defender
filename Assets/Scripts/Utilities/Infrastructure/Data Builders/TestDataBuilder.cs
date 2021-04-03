using UnityEngine;

namespace WizardGame.Utility.Infrastructure.DataBuilders
{
    public abstract class TestDataBuilder<T>
    {
        public abstract T Build();

        public static implicit operator T(TestDataBuilder<T> builder)
        {
            return builder.Build();
        }
    }
}
