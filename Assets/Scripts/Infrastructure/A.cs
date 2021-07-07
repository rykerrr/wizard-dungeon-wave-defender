using WizardGame.Infrastructure.DataBuilders;

namespace WizardGame.Infrastructure
{
    public static class A
    {
        public static DownTimerDataBuilder DownTimer() => new DownTimerDataBuilder();
        public static NullTimerDataBuilder NullTimer() => new NullTimerDataBuilder();
        public static StopwatchDataBuilder Stopwatch() => new StopwatchDataBuilder();
        public static LocomotionMovementDataBuilder LocomotionMovement() => new LocomotionMovementDataBuilder();
        public static JumpingMovementDataBuilder JumpingMovement() => new JumpingMovementDataBuilder();
        public static GravityMovementDataBuilder GravityMovement() => new GravityMovementDataBuilder();
        public static ForceReceiverDataBuilder ForceReceiver() => new ForceReceiverDataBuilder();
    }
}