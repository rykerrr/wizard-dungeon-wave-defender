using WizardGame.Utility.Patterns;
using WizardGame.Utility.Timers;

namespace WizardGame.Infrastructure.DataBuilders
{
    public abstract class TimerDataBuilder<T> : AbstractDataBuilder<T> where T: ITimer
    {
        protected float time;
        
        public abstract override T Build();

        protected TimerDataBuilder(float time)
        {
            this.time = time;
        }

        public TimerDataBuilder<T> WithTime(float time) 
        {
            this.time = time;
            
            return this;
        }
    }
}