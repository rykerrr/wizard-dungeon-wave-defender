using UnityEngine;

namespace WizardGame.CustomEventSystem
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "Events/Void Event")]
    public class VoidGameEvent : BaseGameEvent<VoidData>
    {
        public void Raise() => base.Raise(new VoidData());
    }
}