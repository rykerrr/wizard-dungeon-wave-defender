using UnityEngine;
using WizardGame.Item_System.UI;
using WizardGame.Item_System.Items;

namespace WizardGame.Item_System
{
    public class Hotbar : MonoBehaviour
    {
        [SerializeField] private HotbarSlotUI[] hotbarSlots = new HotbarSlotUI[10];

        public void Add(HotbarItem itemToAdd)
        {
            foreach (var slot in hotbarSlots)
            {
                if (slot.AddItem(itemToAdd)) return;
            }
        }
    }
}