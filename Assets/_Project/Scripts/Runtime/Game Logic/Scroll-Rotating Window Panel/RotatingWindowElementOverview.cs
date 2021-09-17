using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WizardGame.SelectionWindow
{
    public class RotatingWindowElementOverview : MonoBehaviour
    {
        [SerializeField] private Image iconDisplayImage = default;
        [SerializeField] private TextMeshProUGUI nameText = default;
        [SerializeField] private TextMeshProUGUI descText = default;
        
        public SelectionWindowElement SelectedWindowElement { get; private set; }
        
        public void DisplayElement(SelectionWindowElement windowElementUi)
        {
            SelectedWindowElement = windowElementUi;
            
            var element = windowElementUi.Element;
            var img = windowElementUi.IconImage;

            nameText.text = element.Name;
            descText.text = element.Description;
            
            iconDisplayImage.sprite = element.ElementSprite;
            iconDisplayImage.color = element.ElementColor;
            
            iconDisplayImage.type = img.type;
            iconDisplayImage.fillCenter = img.fillCenter;
        }

        public void ClearElement()
        {
            nameText.text = "";
            descText.text = "";

            iconDisplayImage.sprite = null;
            iconDisplayImage.color = Color.white;

            SelectedWindowElement = null;
        }
    }
}