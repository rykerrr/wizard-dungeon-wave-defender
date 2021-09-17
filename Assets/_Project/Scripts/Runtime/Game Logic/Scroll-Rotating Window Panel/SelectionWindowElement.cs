using UnityEngine;
using UnityEngine.UI;

namespace WizardGame.SelectionWindow
{
    public class SelectionWindowElement : MonoBehaviour
    {
        [SerializeField] private WindowElementData data = default;
        
        [SerializeField] private Image iconImage = default;

        public WindowElementData Data
        {
            get => data;
            
            set
            {
                data = value;
                
                UpdateUI();
            }
        }
        public Image IconImage => iconImage;

        public float CurrentAngle { get; set; }

        private void Awake()
        {
            if (ReferenceEquals(iconImage, null)) iconImage = GetComponentInChildren<Image>();
            
            SetNameAsSiblingIndex();
        }

        private void UpdateUI()
        {
            iconImage.sprite = data.Sprite;

            iconImage.color = data.Color;
        }
        
        private void SetNameAsSiblingIndex()
        {
            name = gameObject.name = $"{transform.GetSiblingIndex()}";
        }
    }
}