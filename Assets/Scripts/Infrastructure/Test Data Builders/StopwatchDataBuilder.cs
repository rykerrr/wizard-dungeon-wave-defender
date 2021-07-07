using WizardGame.Utility.Timers;

namespace WizardGame.Infrastructure.DataBuilders
{
    public class StopwatchDataBuilder : TimerDataBuilder<Stopwatch>
    {
        public override Stopwatch Build()
        {
            return new Stopwatch(time);
        }
        
        public StopwatchDataBuilder(float time) : base(time) { }
        
        public StopwatchDataBuilder() : this(0) { }
    }
}