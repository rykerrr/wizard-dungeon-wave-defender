using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    [CreateAssetMenu(menuName = "Status Effect/Status Effect Data", fileName = "New Status Effect Data")]
    public class StatusEffectData : ScriptableObject
    {
        [SerializeField] private new string name = "";

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
        
        public string Name
        {
            get => name;
            private set => name = value;
        }

        public Sprite StatusEffectIcon => statusEffectIcon;
        public float Duration => duration;
        public float MovementSpeedMultiplier => movementSpeedMultuiplier;
        public int DamagePerFrame => damagePerFrame;

        private void SetNameAsAssetFileName()
        {
            var assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
            Name = Path.GetFileNameWithoutExtension(assetPath);
        }
        
        protected virtual void OnValidate()
        {
            SetNameAsAssetFileName();

            isDirty = true;
        }
        
        // Note to self: projectChanged runs every time the file is renamed
        private void OnEnable()
        {
            EditorApplication.projectChanged += SetNameAsAssetFileName;
        }
    }
}