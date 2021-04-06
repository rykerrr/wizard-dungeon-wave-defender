using UnityEngine;
using WizardGame.Timers;

namespace WizardGame.Utility.Infrastructure.DataBuilders
{
    public class StopwatchDataBuilder : ITimerDataBuilder<Stopwatch>
    {
        private float time;
        
        public override Stopwatch Build()
        {
            return new Stopwatch(time);
        }
        
        public StopwatchDataBuilder(float time) : base(time) { }
        
        public StopwatchDataBuilder() : this(0) { }
    }
}