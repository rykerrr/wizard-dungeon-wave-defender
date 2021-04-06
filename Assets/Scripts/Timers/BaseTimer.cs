namespace WizardGame.Timers
{
    public abstract class BaseTimer : ITimer
    {
        public bool IsTimerEnabled { get; private set; } = true;

        public float Time { get; protected set; }
        
        public void EnableTimer() => IsTimerEnabled = true;
        public void DisableTimer() => IsTimerEnabled = false;

        public abstract bool TryTick(float time);
    }
}