using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using WizardGame.Item_System.Items;

namespace WizardGame.Item_System.UI
{
    public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
    {
        [SerializeField] protected Image slotItemIconImage = default;
        [SerializeField] protected TextMeshProUGUI itemQuantText = default;
        public int SlotIndex { get; private set; }
        public virtual HotbarItem ReferencedSlotItem { get; set; }

        private void Start()
        {
            SlotIndex = transform.GetSiblingIndex();
            UpdateSlotUi();
        }

        protected virtual void EnableSlotUI(bool enable)
        {
            slotItemIconImage.enabled = enable;
            
            // cooldown things here
            // weeeeeeee
        }
        
        public abstract void UpdateSlotUi();
        public abstract void OnDrop(PointerEventData eventData);

        private void OnEnable() => UpdateSlotUi();
    }
}