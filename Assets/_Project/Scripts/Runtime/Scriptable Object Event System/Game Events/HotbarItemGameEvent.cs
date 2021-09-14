using UnityEngine;
using WizardGame.Item_System.Items;

namespace WizardGame.CustomEventSystem
{
    [CreateAssetMenu(fileName = "New HotbarItem Event", menuName = "Events/HotbarItem Event")]
    public class HotbarItemGameEvent : BaseGameEvent<HotbarItem>
    {
    }
}
