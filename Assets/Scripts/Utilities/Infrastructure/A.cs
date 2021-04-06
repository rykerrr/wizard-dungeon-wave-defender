using WizardGame.Utility.Infrastructure.DataBuilders;

namespace WizardGame.Utility.Infrastructure
{
    public static class A
    {
        public static MovementInputProcessorBuilder MovementInputProcessor() => new MovementInputProcessorBuilder();
        public static DownTimerDataBuilder DownTimer() => new DownTimerDataBuilder();
        public static NullTimerDataBuilder NullTimer() => new NullTimerDataBuilder();
        public static StopwatchDataBuilder Stopwatch() => new StopwatchDataBuilder();
    }
}