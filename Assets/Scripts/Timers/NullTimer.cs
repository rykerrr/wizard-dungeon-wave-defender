namespace WizardGame.Timers
{
    public class NullTimer : BaseTimer
    {
        public override bool TryTick(float deltaTime) => true;
    }
}