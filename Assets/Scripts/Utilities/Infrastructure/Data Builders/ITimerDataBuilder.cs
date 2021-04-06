using WizardGame.Timers;

namespace WizardGame.Utility.Infrastructure.DataBuilders
{
    public abstract class ITimerDataBuilder<T> : TestDataBuilder<T> where T: ITimer
    {
        protected float time;
        
        public abstract override T Build();

        protected ITimerDataBuilder(float time)
        {
            this.time = time;
        }

        public ITimerDataBuilder<T> WithTime(float time) 
        {
            this.time = time;
            
            return this;
        }
    }
}