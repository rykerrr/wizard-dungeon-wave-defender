using UnityEngine;

namespace WizardGame.Utility.Timers
{
    public class Stopwatch : BaseTimer
    {
        public Stopwatch(float time)
        {
            Time = time;
        }

        public override bool TryTick(float deltaTime)
        {
            if (deltaTime < 0 || !IsTimerEnabled) return false;
        
            Time = Mathf.Max(Time + deltaTime, 0);
            return true;
        }

        public override void Reset()
        {
            Time = 0;
        }
    }
}