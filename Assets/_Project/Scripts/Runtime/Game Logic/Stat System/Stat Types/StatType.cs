using System.IO;
using UnityEditor;
using UnityEngine;

namespace WizardGame.Stats_System
{
    public abstract class StatType : ScriptableObject
    {
        public abstract string Name { get; protected set; }
        public abstract int Value { get; }

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

        private void OnDisable()
        {
            EditorApplication.projectChanged -= SetNameAsAssetFileName;
        }

        public abstract override string ToString();
    }
}