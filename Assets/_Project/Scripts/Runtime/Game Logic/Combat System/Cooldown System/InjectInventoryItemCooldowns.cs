using System.Linq;
using UnityEngine;
using WizardGame.Item_System.Item_Containers;

namespace WizardGame.Combat_System.Cooldown_System
{
    public class InjectInventoryItemCooldowns : MonoBehaviour
    {
        [SerializeField] private Inventory inv = default;
        [SerializeField] private CooldownSystem cdSys = default;

        private void Awake()
        {
            cdSys.InitializeCooldowns(inv.ItemContainer.ItemSlots.Where(x => !ReferenceEquals(x.invItem, null))
                .Select(x => (IHasCooldown)x.invItem.CooldownData).ToArray());
        }
    }
}