using UnityEngine;
using WizardGame.Timers;

namespace WizardGame.Utility.Infrastructure.DataBuilders
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