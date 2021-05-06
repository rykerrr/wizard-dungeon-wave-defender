using UnityEngine;

namespace WizardGame.Timers
{
    public class DownTimer : BaseTimer
    {
        public DownTimer(float time)
        {
            SetTimer(time);
        }

        public void SetTimer(float time)
        {
            Time = Mathf.Max(time, 0);
        }

        public override bool TryTick(float deltaTime)
        {
            var timerFinished = Time == 0;
            var deltaTimeIsNegative = deltaTime < 0;
            
            if (timerFinished || deltaTimeIsNegative || !IsTimerEnabled) return false;
        
            Time = Mathf.Max(Time - deltaTime, 0);
            return true;
        }
    }
}