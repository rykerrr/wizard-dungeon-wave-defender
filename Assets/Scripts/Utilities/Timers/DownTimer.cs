using System;
using UnityEngine;

namespace WizardGame.Utility.Timers
{
    public class DownTimer : BaseTimer
    {
        private float defaultTime = default;

        public Action OnTimerEnd { get; set; } = delegate { };

        public DownTimer(float time)
        {
            SetTimer(time);
        }

        public void SetTimer(float time)
        {
            defaultTime = Mathf.Max(time, 0);

            Time = defaultTime;
        }

        public override bool TryTick(float deltaTime)
        {
            var timerFinished = Time == 0;
            var deltaTimeIsNegative = deltaTime < 0;
            
            if (timerFinished || deltaTimeIsNegative || !IsTimerEnabled) return false;
        
            Time = Mathf.Max(Time - deltaTime, 0);

            if (Time == 0) OnTimerEnd?.Invoke();
            
            return true;
        }

        public override void Reset()
        {
            Time = defaultTime;
        }
    }
}