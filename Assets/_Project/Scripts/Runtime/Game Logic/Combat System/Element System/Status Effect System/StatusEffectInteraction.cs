using System.IO;
using UnityEditor;
using UnityEngine;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    [CreateAssetMenu(menuName = "Status Effect/Status Effect Interaction", fileName = "New Status Effect Interaction")]
    public class StatusEffectInteraction : ScriptableObject
    {
        [Tooltip("Name convention: [Base][Target][Type], Interaction for fire to cancel water: FireWaterCancel")]
        [SerializeField] private new string name;
        
        [SerializeField] private StatusEffectData target = default;
        [SerializeField] private InteractionType interactionType = default;
        [SerializeField] private StatusEffectData result = default;

        [SerializeField] private float effectiveness;

        public string Name
        {
            get => name;
            private set => name = value;
        }
        
        public StatusEffectData Target => target;
        public InteractionType InteractionType => interactionType;
        public StatusEffectData Result => result;
        public float Effectiveness => effectiveness;
        
        private void SetNameAsAssetFileName()
        {
            var assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
            Name = Path.GetFileNameWithoutExtension(assetPath);
        }
        
        protected virtual void OnValidate()
        {
            SetNameAsAssetFileName();
        }
        
        // Note to self: projectChanged runs every time the file is renamed
        private void OnEnable()
        {
            EditorApplication.projectChanged += SetNameAsAssetFileName;
        }
        
        public static int CompareInteractionType(StatusEffectInteraction x, StatusEffectInteraction y)
        {
            if ((int) x.InteractionType > (int) y.InteractionType) return 1;
            if (x.InteractionType == y.InteractionType) return 0;
            
            return -1;
        }
    }
}