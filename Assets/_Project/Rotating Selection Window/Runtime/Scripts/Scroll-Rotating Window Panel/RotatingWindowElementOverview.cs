using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SelectionWindow
{
    public class RotatingWindowElementOverview : MonoBehaviour
    {
        [SerializeField] private Image iconDisplayImage = default;
        [SerializeField] private TextMeshProUGUI nameText = default;
        [SerializeField] private TextMeshProUGUI descText = default;
        
        public SelectionWindowElement SelectedElement { get; private set; }
        
        public void DisplayElement(SelectionWindowElement elem)
        {
            SelectedElement = elem;
            
            var elemData = elem.Data;
            var img = elem.IconImage;

            nameText.text = elemData.Name;
            descText.text = elemData.Description;
            
            iconDisplayImage.sprite = elemData.Sprite;
            iconDisplayImage.color = elemData.Color;
            
            iconDisplayImage.type = img.type;
            iconDisplayImage.fillCenter = img.fillCenter;
        }

        public void ClearElement()
        {
            nameText.text = "";
            descText.text = "";

            iconDisplayImage.sprite = null;
            iconDisplayImage.color = Color.white;

            SelectedElement = null;
        }
    }
}