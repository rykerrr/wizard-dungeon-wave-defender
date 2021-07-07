using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.CustomEventSystem
{
    [CreateAssetMenu(fileName = "New Float Event", menuName = "Events/Float Event")]
    public class FloatGameEvent : BaseGameEvent<float>
    {

    }
}
