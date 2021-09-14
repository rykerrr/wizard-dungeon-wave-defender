using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.CustomEventSystem
{
    public class BaseGameEvent<T> : ScriptableObject
    {
        private List<IGameEventListener<T>> eventListeners = new List<IGameEventListener<T>>();

        public void Raise(T passValue)
        {
            foreach (var listener in eventListeners)
            {
                listener.OnEventRaised(passValue);
            }
        }
        
        public bool RegisterListener(IGameEventListener<T> newListener)
        {
            if (eventListeners.Contains(newListener)) return false;
            
            eventListeners.Add(newListener);
            return true;
        }

        public bool UnregisterListener(IGameEventListener<T> listenerToRemove)
        {
            return eventListeners.Remove(listenerToRemove);
        }
    }
}
