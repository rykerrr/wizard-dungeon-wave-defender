namespace WizardGame.CustomEventSystem
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T passValue);
    }
}