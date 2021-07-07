using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using WizardGame.Item_System.Items;
using WizardGame.Item_System.UI;

namespace WizardGame.Item_System.Tooltip
{
    public class ItemTooltipPopup : MonoBehaviour
    {
        [Header("Properties")]
        [SerializeField] private bool followPrevObj = false;
        [Header("References")]
        [SerializeField] private Canvas popupCanvas = default;
        [SerializeField] private RectTransform popupObjTransform = default;
        [SerializeField] private RectTransform backgroundObjTransform = default;
        [SerializeField] private TextMeshProUGUI tooltipInfoText = default;

        private RectTransform prevObj = default;
        private StringBuilder sb = new StringBuilder();

        private void Update()
        {
            if (!popupObjTransform.gameObject.activeSelf) return;

            if (followPrevObj)
            {
                FollowPrevObject();
            }
            else
            {
                FollowCursor();
            }
        }

        private void FollowCursor()
        {
            Vector2 mPos = Mouse.current.position.ReadValue();
            Vector2 newPos = mPos;
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

        private void FollowPrevObject()
        {
            if (ReferenceEquals(prevObj, null)) return;
            
            Rect canvRect = popupCanvas.pixelRect;
            Rect backgrObjRect = backgroundObjTransform.rect;
            Rect prevObjRect = prevObj.rect;
            float scaleFact = popupCanvas.scaleFactor;
            
            Vector2 newPos = (Vector2)prevObj.position + new Vector2(-prevObjRect.width, prevObjRect.height) / 4;

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

        public void ShowTooltip(ItemSlotUI item)
        {
            UpdateTooltipText(item.ReferencedSlotItem);

            prevObj = (RectTransform)item.transform;
            popupObjTransform.gameObject.SetActive(true);
        }

        public void HideTooltip()
        {
            popupObjTransform.gameObject.SetActive(false);
        }
        
        private void UpdateTooltipText(HotbarItem item)
        {
            sb.Append("<size=35>").Append(item.ColouredName).Append("</size>").AppendLine();
            sb.Append(item.GetInfoDisplayText());

            tooltipInfoText.text = sb.ToString();
            sb.Clear();
            
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)popupCanvas.transform);
        }
    }
}