using WizardGame.Utility.Timers;

namespace WizardGame.Infrastructure.DataBuilders
{
    public class NullTimerDataBuilder : TimerDataBuilder<NullTimer>
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
