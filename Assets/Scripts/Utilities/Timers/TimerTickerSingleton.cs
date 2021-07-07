using System.Collections.Generic;
using UnityEngine;
using WizardGame.Utility.Patterns;

namespace WizardGame.Utility.Timers
{
    public class TimerTickerSingleton : InheritableSingleton<TimerTickerSingleton>
    {
        private readonly List<ITimer> timers = new List<ITimer>();

        public List<ITimer> Timers => timers;

        private void Update()
        {
            TickAllTimers();
        }

        private void TickAllTimers()
        {            float time = Time.deltaTime;

            
            for (int i = timers.Count - 1; i >= 0; i--)
            {
                timers[i].TryTick(time);
            }
        }

        public void AddTimer(ITimer timer)
        {
            if (timers.Contains(timer)) return;
            
            timers.Add(timer);
        }

        public bool RemoveTimer(ITimer timer)
            => timers.Remove(timer);

        [ContextMenu("Text dump timers")]
        public void TextDumpTimers()
        {
            foreach (var timer in timers)
            {
                Debug.Log(timer);
            }
        }
    }
}