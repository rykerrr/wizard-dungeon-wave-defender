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
        
        [SerializeField] private Transform owner; // field is temporary until i figure out what file it
        // fits in best
        
        public int SlotIndexOnUI { get; private set; }
        public virtual HotbarItem ReferencedSlotItem { get; set; }

        public Transform Owner => owner;
        
        private void Start()
        {
            SlotIndexOnUI = transform.GetSiblingIndex();
            UpdateSlotUi();
        }

        protected virtual void EnableSlotUI(bool enable)
        {
            slotItemIconImage.enabled = enable;
            
            // cooldown things here
        }
        
        public abstract void UpdateSlotUi();
        public abstract void OnDrop(PointerEventData eventData);

        private void OnEnable() => UpdateSlotUi();
    }
}