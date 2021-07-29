using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using WizardGame.Item_System.Tooltip;

namespace WizardGame.Item_System.UI
{
    public class ItemDragHandler : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected ItemSlotUI itemSlotUI;
        // rename the below field
        [SerializeField] protected Transform parentForDrag = default;
        [SerializeField] protected ItemTooltipPopup tooltipPopup = default;
        
        private CanvasGroup canvGroup = default;
        private Transform actualParent = default;
        private bool isHovering = false;

        public bool IsHovering => isHovering;
        public ItemSlotUI ItemSlotUI => itemSlotUI;

        private RectTransform thisTransform = default;
        private RectTransform itemSlotTransform = default;
        
        private void Awake()
        {
            thisTransform = (RectTransform)transform;
            itemSlotTransform = (RectTransform) ItemSlotUI.transform;
            
            if (canvGroup == null)
            {
                canvGroup = GetComponent<CanvasGroup>();
            }
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            thisTransform.position = Mouse.current.position.ReadValue();
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log(thisTransform.parent + " | " + actualParent, this);
            thisTransform.SetParent(actualParent);
            thisTransform.SetSiblingIndex(1);

            thisTransform.anchoredPosition = Vector2.zero;
            
            canvGroup.blocksRaycasts = true;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            // raise event for dragging thing

            actualParent = thisTransform.parent;

            thisTransform.SetParent(parentForDrag);
            thisTransform.SetAsLastSibling();

            canvGroup.blocksRaycasts = false;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            isHovering = true;
            
            tooltipPopup.ShowTooltip(ItemSlotUI);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            isHovering = false;

            tooltipPopup.HideTooltip();
        }
        
        private void OnDisable()
        {
            isHovering = false;
        }
    }
}