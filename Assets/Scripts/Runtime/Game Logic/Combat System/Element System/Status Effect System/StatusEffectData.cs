using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    [CreateAssetMenu(menuName = "Status Effect/Status Effect Data", fileName = "New Status Effect Data")]
    public class StatusEffectData : ScriptableObject
    {
        [Tooltip("Not the type name itself, but just the prefix (e.g \"Wet\", \"OnFire\")")]
        [SerializeField] private new string name = "";
        
        private static string namespacePath = "WizardGame.Combat_System.Element_System.Status_Effects";
        
        // Most serializable System.Type variations I've seen convert the name to a type
        // Seems a bit overkill to add an entire class for that as of now
        
        [Header("Stat effect data")]
        [SerializeField] private float duration;
        [SerializeField] private float movementSpeedMultuiplier = 1f;
        [SerializeField] private int damagePerFrame = 0;
        [SerializeField] private StatusEffectStackType stackType;
        
        private Type statusEffectType = null;
        public StatusEffectStackType StackType => stackType;

        public string Name
        {
            get => name;
            private set => name = value;
        }
        
        public float Duration => duration;
        public float MovementSpeedMultiplier => movementSpeedMultuiplier;
        public int DamagePerFrame => damagePerFrame;

        public Type StatEffectType
        {
            get
            {
                statusEffectType ??= Type.GetType($"{namespacePath}.{Name}StatusEffect");

                return statusEffectType;
            }
        }

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
    }
}