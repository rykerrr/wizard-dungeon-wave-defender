using UnityEngine;
using WizardGame.Combat_System.Element_System.Status_Effects;
using WizardGame.Utility;

namespace WizardGame.Combat_System.Element_System
{
    [CreateAssetMenu(menuName = "Elements/New Element", fileName = "New Element")]
    public class Element : ScriptableObjectAutoNameSet
    {
        [SerializeField] [Multiline] private string description = "Description";
            
        [SerializeField] private Sprite elementSprite = default;
        [SerializeField] private Color elementColor;
        
        [SerializeField] private ElementSpellData elementSpellData = default;
        [SerializeField] private StatusEffectData statusEffectToApply = default;

        public string Description => description;
        
        public ElementSpellData ElementSpellData => elementSpellData;
        public StatusEffectData StatusEffectToApply => statusEffectToApply;
        public Sprite ElementSprite => elementSprite;
        public Color ElementColor => elementColor;
    }
}