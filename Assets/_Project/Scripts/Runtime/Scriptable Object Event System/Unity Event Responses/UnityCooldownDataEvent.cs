using System;
using UnityEngine.Events;
using WizardGame.Combat_System.Cooldown_System;

namespace WizardGame.CustomEventSystem
{
    [Serializable]
    public class UnityCooldownDataEvent : UnityEvent<Cooldown>
    {
        
    }
}