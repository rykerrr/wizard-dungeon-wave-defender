using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using WizardGame.Item_System.Item_Containers;
using WizardGame.Item_System.Items;
using WizardGame.Tooltips;

namespace WizardGame.Item_System.UI
{
    public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
    {
        [SerializeField] protected Image slotItemIconImage = default;
        [SerializeField] protected TextMeshProUGUI itemQuantText = default;
        [SerializeField] protected CooldownDisplay cdDisplay = default;
        
        [SerializeField] private Transform owner; // field is temporary until i figure out what file it
        // fits in best
        [SerializeField] protected Inventory inventory;

        public Inventory Inventory => inventory;
        public CooldownDisplay CdDisplay => cdDisplay;
        public int SlotIndexOnUI { get; private set; }
        public virtual HotbarItem ReferencedSlotItem { get; protected set; }

        public Transform Owner => owner;
        
        private void Start()
        {
            SlotIndexOnUI = transform.GetSiblingIndex();
            UpdateSlotUi();

            if (ReferenceEquals(ReferencedSlotItem, null)) return;
            CdDisplay.UpdateData(ReferencedSlotItem.CooldownData);
        }

        protected virtual void EnableSlotUI(bool enable)
        {
            slotItemIconImage.enabled = enable;
        }

        public void UseReferencedItem()
        {
            if (ReferenceEquals(ReferencedSlotItem, null)) return;

            ReferencedSlotItem.UseItem();
        }

        public abstract void UpdateSlotUi();
        public abstract void OnDrop(PointerEventData eventData);

        private void OnEnable() => UpdateSlotUi();
        
        #region debug
        #if UNITY_EDITOR
        [ContextMenu("Check if item null")]
        public void CheckIfItemNull()
        {
            var itemIsNull = ReferenceEquals(ReferencedSlotItem, null);
            
            Debug.Log($"Item: {ReferencedSlotItem}, is null: {itemIsNull}");
        }
        
        [ContextMenu("Test UseReferencedItem for given item (Will not be used if null)")]
        public void TestUseReferencedItem()
        {
            UseReferencedItem();
        }
        #endif
        #endregion
    }
}