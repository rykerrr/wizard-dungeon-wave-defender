using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace WizardGame.Tooltips
{
    public abstract class TooltipPopupBase : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private bool stayOnHoveredObj = false;
        [Header("References")]
        [SerializeField] protected Canvas popupCanvas = default;
        [SerializeField] protected RectTransform popupObjTransform = default;
        [SerializeField] protected RectTransform backgroundObjTransform = default;
        [SerializeField] protected TextMeshProUGUI tooltipInfoText = default;
        [SerializeField] protected VerticalLayoutGroup tooltipLayoutGroup = default;
        
        protected RectTransform prevObj = default;

        private void Update()
        {
            if (!popupObjTransform.gameObject.activeSelf) return;
            
            if (stayOnHoveredObj)
            {
                StayOnHoveredObject();
            }
            else
            {
                FollowCursor();
            }
        }

        private void FollowCursor()
        {
            Vector2 mPos = Mouse.current.position.ReadValue();
            
            KeepWindowInBounds(mPos);
        }
        
        private void StayOnHoveredObject()
        {
            if (ReferenceEquals(prevObj, null)) return;
            
            Rect prevObjRect = prevObj.rect;
            Vector2 newPos = (Vector2)prevObj.position + new Vector2(-prevObjRect.width, prevObjRect.height) / 4;

            KeepWindowInBounds(newPos);
        }

        private void KeepWindowInBounds(Vector2 newPos)
        {
            Rect canvRect = popupCanvas.pixelRect;
            Rect backgrObjRect = backgroundObjTransform.rect;
            float scaleFact = popupCanvas.scaleFactor;

            float distToLeftEdge = newPos.x - backgrObjRect.width * scaleFact;
            float distToRightEdge = canvRect.width - newPos.x;
            float distToBotEdge = newPos.y;
            float distToTopEdge = canvRect.height - (newPos.y + backgrObjRect.height * scaleFact);

            // Debug.Log(string.Join(" | ", distToLeftEdge, distToRightEdge, distToTopEdge, mPos, canvRect.width, canvRect.height));
            if (distToRightEdge < 0)
            {
                newPos += new Vector2(distToRightEdge, 0);
            }
            else if (distToLeftEdge < 0)
            {
                newPos -= new Vector2(distToLeftEdge, 0);
            }

            if (distToTopEdge < 0)
            {
                newPos += new Vector2(0, distToTopEdge);
            }
            else if (distToBotEdge < 0)
            {
                newPos -= new Vector2(0, distToBotEdge);
            }

            popupObjTransform.position = newPos;
        }

        protected void ShowTooltip()
        {
            popupObjTransform.gameObject.SetActive(true);
        }
        
        public void HideTooltip()
        {
            popupObjTransform.gameObject.SetActive(false);

            prevObj = null;
        }

        protected void UpdateTooltipText(string text)
        {
            tooltipInfoText.text = text;
            
            tooltipLayoutGroup.enabled = false;
            LayoutRebuilder.ForceRebuildLayoutImmediate(popupObjTransform);
            tooltipLayoutGroup.enabled = true;
        }
    }
}