using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.CustomEventSystem
{
    [CreateAssetMenu(fileName = "New Bool Event", menuName = "Events/Bool Event")]
    public class BoolGameEvent : BaseGameEvent<bool>
    {

    }
}
