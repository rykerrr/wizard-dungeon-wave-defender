using UnityEngine;
using UnityEngine.UI;
using WizardGame.Combat_System.Element_System;

namespace WizardGame.SelectionWindow
{
    public class SelectionWindowElement : MonoBehaviour
    {
        [SerializeField] private Element element = default;
        
        [SerializeField] private Image iconImage = default;

        public Element Element
        {
            get => element;
            
            set
            {
                element = value;
                
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
            iconImage.sprite = element.ElementSprite;

            iconImage.color = element.ElementColor;
        }
        
        private void SetNameAsSiblingIndex()
        {
            name = gameObject.name = $"{transform.GetSiblingIndex()}";
        }
    }
}