using System.Collections.Generic;
using UnityEngine;
using WizardGame.Utility;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    [CreateAssetMenu(menuName = "Status Effect/Status Effect Data", fileName = "Status Effect Data")]
    public class StatusEffectData : ScriptableObjectAutoNameSet
    {
        [Header("Stat effect data")] 
        [SerializeField] private List<StatusEffectInteraction> interactions = 
            new List<StatusEffectInteraction>();
        [SerializeField] private Sprite statusEffectIcon = default;
        [SerializeField] private float duration = default;
        [SerializeField] private float movementSpeedMultuiplier = 1f;
        [SerializeField] private int damagePerFrame = 0;
        [SerializeField] private StatusEffectStackType stackType;
        
        public StatusEffectStackType StackType => stackType;

        private bool isDirty = true;

        // Accessed rarely
        public List<StatusEffectInteraction> Interactions
        {
            get
            {
                if (isDirty)
                {
                    interactions.Sort(StatusEffectInteraction.CompareInteractionType);
                    isDirty = false;
                }

                return interactions;
            }
        }
        
        public Sprite StatusEffectIcon => statusEffectIcon;
        public float Duration => duration;
        public float MovementSpeedMultiplier => movementSpeedMultuiplier;
        public int DamagePerFrame => damagePerFrame;

        protected override void OnValidateUtility()
        {
            isDirty = true;
        }
    }
}