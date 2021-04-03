using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.CustomEventSystem
{
    [CreateAssetMenu(fileName = "New Int Event", menuName = "Events/Int Event")]
    public class IntGameEvent : BaseGameEvent<int>
    {
        
    }
}
