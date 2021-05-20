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
        [SerializeField] protected Transform parentForDraggingAroundInv = default;
        [SerializeField] protected ItemTooltipPopup tooltipPopup = default;
        
        public ItemSlotUI ItemSlotUI => itemSlotUI;

        private CanvasGroup canvGroup = default;
        private Transform prevParent = default;
        private bool isHovering = false;

        private void Awake()
        {
            if (canvGroup == null)
            {
                canvGroup = GetComponent<CanvasGroup>();
            }
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            transform.position = Mouse.current.position.ReadValue();
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            Transform thisTransform = transform;

            thisTransform.SetParent(prevParent);
            thisTransform.localPosition = Vector3.zero;

            canvGroup.blocksRaycasts = true;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;

            // raise event for dragging thing

            prevParent = transform.parent;

            transform.SetParent(parentForDraggingAroundInv);

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