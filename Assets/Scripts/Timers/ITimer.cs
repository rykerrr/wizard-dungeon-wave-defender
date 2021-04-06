namespace WizardGame.Timers
{
    public interface ITimer
    {
        float Time { get; }
        bool IsTimerEnabled { get; }

        bool TryTick(float deltaTime);
        void EnableTimer();
        void DisableTimer();
    }
}