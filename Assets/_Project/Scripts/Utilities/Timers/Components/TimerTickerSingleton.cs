using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using WizardGame.Utility.Patterns;

namespace WizardGame.Utility.Timers
{
    public class TimerTickerSingleton : InheritableSingleton<TimerTickerSingleton>
    {
        private readonly Dictionary<object, ITimer> timers = new Dictionary<object, ITimer>();

        private void Update()
        {
            TickAllTimers();
        }

        private void TickAllTimers()
        {
            float time = Time.deltaTime;
            
            // Purpose of this is the same as a reverse for loop, if a timer is removed, shifting would occur
            // If we iterate backwards we won't get a CollectionWasModifier error
            // Placeholder to prevent table head smacking
            foreach (var kvp in timers.Reverse())
            {
                kvp.Value.TryTick(time);
            }
        }

        public void AddTimer(ITimer timer, object key)
        {
            // Tiny problem with how the timers are set
            // Dictionaries require a non-null key
            
            if (key == null || timers.ContainsKey(key)) return;
            
            timers.Add(key, timer);
        }

        public ITimer GetTimer(object key)
        {
            if (timers.ContainsKey(key)) return timers[key];

            return null;
        }

        public bool RemoveTimer(object key)
        {
            if (!timers.ContainsKey(key)) return false;
            
            return timers.Remove(key);
        }

        public bool RemoveTimer(ITimer value)
        {
            if (value == null) return false;
            
            var kvpToRemove = timers.FirstOrDefault(x => x.Value == value);

            return timers.Remove(kvpToRemove.Key);
        }

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