using System.IO;
using UnityEditor;
using UnityEngine;
using WizardGame.Combat_System.Element_System.Status_Effects;

namespace WizardGame.Combat_System.Element_System
{
    [CreateAssetMenu(menuName = "Elements/New Element", fileName = "New Element")]
    public class Element : ScriptableObject
    {
        [SerializeField] private new string name = "New Element";
        [SerializeField] private Sprite elementSprite = default;
        [SerializeField] private Color elementColor;
        
        [SerializeField] private ElementSpellData elementSpellData = default;
        [SerializeField] private StatusEffectData statusEffectToApply = default;
        
        public string Name
        {
            get => name;
            private set => name = value;
        }

        public ElementSpellData ElementSpellData => elementSpellData;
        public Sprite ElementSprite => elementSprite;
        public Color ElementColor => elementColor;
        
        private void SetNameAsAssetFileName()
        {
            var assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
            Name = Path.GetFileNameWithoutExtension(assetPath);
        }
        
        private void OnValidate()
        {
            SetNameAsAssetFileName();
        }

        private void OnEnable()
        {
            EditorApplication.projectChanged += SetNameAsAssetFileName;
        }

        private void OnDisable()
        {
            EditorApplication.projectChanged -= SetNameAsAssetFileName;
        }
    }
}