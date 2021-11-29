using UnityEngine;
using WizardGame.Utility;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    [CreateAssetMenu(menuName = "Status Effect/Status Effect Interaction", fileName = "New Status Effect Interaction")]
    public class StatusEffectInteraction : ScriptableObjectAutoNameSet
    {
        [SerializeField] private StatusEffectData target = default;
        [SerializeField] private InteractionType interactionType = default;
        [SerializeField] private StatusEffectData result = default;

        [SerializeField] private float effectiveness;
        
        public StatusEffectData Target => target;
        public InteractionType InteractionType => interactionType;
        public StatusEffectData Result => result;
        public float Effectiveness => effectiveness;

        public static int CompareInteractionType(StatusEffectInteraction x, StatusEffectInteraction y)
        {
            if ((int) x.InteractionType > (int) y.InteractionType) return 1;
            if (x.InteractionType == y.InteractionType) return 0;
            
            return -1;
        }
    }
}