using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 0649
namespace WizardGame.CustomEventSystem
{
    public class BaseGameEventListener<T, E, UER> : MonoBehaviour, IGameEventListener<T>
        where E : BaseGameEvent<T> where UER : UnityEvent<T>
    {
        [SerializeField] private E gameEvent = null;
        [SerializeField] private UER eventResponse = null;

        private bool GameEventIsNull => gameEvent == null;
        
        public void OnEventRaised(T passValue)
        {
            eventResponse?.Invoke(passValue);
        }

        private void OnEnable()
        {
            if (GameEventIsNull) return;

            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (GameEventIsNull) return;

            gameEvent.UnregisterListener(this);
        }
    }
}