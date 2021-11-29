using System;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Combat_System.Cooldown_System;
using WizardGame.Item_System.UI;
using WizardGame.Item_System.Items;

namespace WizardGame.Item_System
{
    public class HotbarBehaviour : MonoBehaviour
    {
        [Header("Swap Item Cooldown Properties")]
        [SerializeField] private int swapItemCooldownId;
        [SerializeField] private int cooldownDuration;
        
        [Header("References")]
        [SerializeField] private HotbarSlotUI[] hotbarSlots = new HotbarSlotUI[10];
        
        public int Id => swapItemCooldownId;

        private int slotIndex = 0;

        private void Update()
        {
            var equip = CheckForInput();
            
            if (equip != -1)
            {
                hotbarSlots?[equip].UseReferencedItem();
            }
        }

        public void Add(HotBarItem itemToAdd)
        {
            foreach (var slot in hotbarSlots)
            {
                if (slot.AddItem(itemToAdd)) return;
            }
        }

        private int CheckForInput()
        {
            int prevSlotIndex = slotIndex;
            
            var curKb = Keyboard.current;

            if (curKb.digit1Key.wasPressedThisFrame)
            {
                slotIndex = 1;
            }
            else if (curKb.digit2Key.wasPressedThisFrame)
            {
                slotIndex = 2;
            }
            else if (curKb.digit3Key.wasPressedThisFrame)
            {
                slotIndex = 3;
            }
            else if (curKb.digit4Key.wasPressedThisFrame)
            {
                slotIndex = 4;
            }
            else if (curKb.digit5Key.wasPressedThisFrame)
            {
                slotIndex = 5;
            }
            else if (curKb.digit6Key.wasPressedThisFrame)
            {
                slotIndex = 6;
            }
            else if (curKb.digit7Key.wasPressedThisFrame)
            {
                slotIndex = 7;
            }
            else if (curKb.digit8Key.wasPressedThisFrame)
            {
                slotIndex = 8;
            }
            else if (curKb.digit9Key.wasPressedThisFrame)
            {
                slotIndex = 9;
            }
            else
            {
                slotIndex = -1;
            }
            
            return prevSlotIndex == slotIndex ? -1 : slotIndex;
        }
        
        public void ScrollThroughSlots(InputAction.CallbackContext ctx)
        {
            float input = ctx.ReadValue<float>();
            int addition = Mathf.Clamp((int)input, -1, 1);
            
            slotIndex = Mathf.Clamp(slotIndex + addition, 0, hotbarSlots.Length - 1);
            
            SelectSlot();
        }

        private void SelectSlot()
        {
            hotbarSlots[slotIndex].UseReferencedItem();
        }
        
        // input: equip/select slot via 1-9
        // input: equip/select by scroll wheel
    }
}