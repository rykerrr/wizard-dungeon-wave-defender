using UnityEngine;
using WizardGame.Timers;

namespace WizardGame.Utility.Infrastructure.DataBuilders
{
    public class DownTimerDataBuilder : ITimerDataBuilder<DownTimer>
    {
        public override DownTimer Build()
        {
            return new DownTimer(time);
        }

        public DownTimerDataBuilder(float time) : base(time) { }
        public DownTimerDataBuilder() : this(0) { }
    }
}