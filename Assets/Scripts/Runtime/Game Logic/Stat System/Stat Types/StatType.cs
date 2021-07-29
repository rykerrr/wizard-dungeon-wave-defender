using System.IO;
using UnityEditor;
using UnityEngine;

namespace WizardGame.Stats_System
{
    public abstract class StatType : ScriptableObject
    {
        public abstract string Name { get; protected set; }
        public abstract int Value { get; }

        protected virtual void OnValidate()
        {
            var assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
            Name = Path.GetFileNameWithoutExtension(assetPath);
        }
        
        // Note to self: projectChanged runs every time the file is renamed
        private void OnEnable()
        {
            EditorApplication.projectChanged += OnValidate;
        }

        private void OnDisable()
        {
            EditorApplication.projectChanged -= OnValidate;
        }

        public abstract override string ToString();
    }
}