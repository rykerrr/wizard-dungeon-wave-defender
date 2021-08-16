using System;
using UnityEngine;

namespace WizardGame.Utility.Timers
{
    public class DownTimer : BaseTimer
    {
        private float defaultTime = default;
        public float DefaultTime => defaultTime;
        
        public Action OnTimerEnd { get; set; } = delegate { };

        public DownTimer(float time)
        {
            SetNewDefaultTime(time);
            
            Reset();
        }

        public void SetNewDefaultTime(float time)
        {
            defaultTime = Mathf.Max(time, 0);

            // Time = defaultTime;
        }

        public override bool TryTick(float deltaTime)
        {
            var timerFinished = Time == 0;
            var deltaTimeIsNegative = deltaTime < 0;
            
            if (timerFinished || deltaTimeIsNegative || !IsTimerEnabled) return false;
            
            Time = Mathf.Max(Time - deltaTime, 0);

            // If time is equal to 0, this should run, only THEN should we be able to check if it's 0 in the first line of this
            // method
            // Unless it starts off at 0
            // Then what the flustertruck is going on

            if (Time == 0)
            {
                Debug.Log("Invoking OnTimerEnd");
                OnTimerEnd?.Invoke();
            }
            
            return true;
        }

        public override void Reset()
        {
            Time = defaultTime;
        }
    }
}