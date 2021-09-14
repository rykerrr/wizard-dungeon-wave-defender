using UnityEngine;
using WizardGame.Utility.Timers;

namespace WizardGame.Infrastructure.DataBuilders
{
    public class DownTimerDataBuilder : TimerDataBuilder<DownTimer>
    {
        public override DownTimer Build()
        {
            return new DownTimer(time);
        }

        public DownTimerDataBuilder(float time) : base(time) { }
        public DownTimerDataBuilder() : this(0) { }
    }
}