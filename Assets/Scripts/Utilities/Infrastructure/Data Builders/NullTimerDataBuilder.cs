using UnityEngine;
using WizardGame.Timers;

namespace WizardGame.Utility.Infrastructure.DataBuilders
{
    public class NullTimerDataBuilder : ITimerDataBuilder<NullTimer>
    {
        public NullTimerDataBuilder() : base(0)
        {
        }

        public override NullTimer Build()
        {
            return new NullTimer();
        }
    }
}
