using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace WizardGame.Item_System.UI
{
    public class ItemDragHandler : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler, IBeginDragHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected ItemSlotUI itemSlotUI;
        // rename the below field
        [SerializeField] protected Transform parentForDrag = default;
        
        private CanvasGroup canvGroup = default;
        private Transform actualParent = default;
        private bool isHovering = false;

        public bool IsHovering => isHovering;
        public ItemSlotUI ItemSlotUI => itemSlotUI;

        private RectTransform thisTransform = default;
        private RectTransform itemSlotTransform = default;
        
        protected virtual void Awake()
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
            thisTransform.SetParent(actualParent);
            thisTransform.SetSiblingIndex(1);
            
            thisTransform.anchoredPosition = Vector2.zero;

            var refSlotItem = itemSlotUI.ReferencedSlotItem;
            ItemSlotUI.CdDisplay.UpdateData(refSlotItem.CooldownData);
            
            canvGroup.blocksRaycasts = true;
        }

        // TODO: Check out "Horrors of OnPointerDown versus OnBeginDrag in Unity3D"
        // https://newbedev.com/horrors-of-onpointerdown-versus-onbegindrag-in-unity3d
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            
            // raise event for dragging thing

            actualParent = thisTransform.parent;

            thisTransform.SetParent(parentForDrag);
            thisTransform.SetAsLastSibling();

            canvGroup.blocksRaycasts = false;
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            ItemSlotUI.CdDisplay.ClearCooldownData();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            isHovering = true;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            isHovering = false;
        }
        
        private void OnDisable()
        {
            isHovering = false;
        }
    }
}