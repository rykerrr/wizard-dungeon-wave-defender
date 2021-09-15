using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Talent_Tree
{
    public abstract class TalentLevelUpAction : ScriptableObject
    {
        [SerializeField] protected new string name = "";

        public string Name => name;
        
        private void SetNameAsAssetFileName()
        {
            var assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
                
            name = Path.GetFileNameWithoutExtension(assetPath);
        }

        // load filename into name
        // + in OnValidate
        protected virtual void OnValidate()
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
        
        public abstract void LevelUp();
    }
}