using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Talent_Tree
{
    [CreateAssetMenu(menuName = "Talents/New Dynamic Talent Asset", fileName = "New Dynamic Talent Asset")]
    public class Talent : ScriptableObject
    {
        [Header("Talent data")]
        [SerializeField] private new string name = default;
        [SerializeField] private string description = default;
        [SerializeField] private int maxTalentLevel = 3;
        [SerializeField] private int singleLevelWeight = 1;
        // Add thing that you unlock and what happens when you unlock it
        // what happens when you unlock it may require talent polymorphism
        [SerializeField] private Sprite icon = default;

        [Header("Talent links")] 
        [SerializeField] private List<TalentLink> links;
        public string Name => name;
        public string Description => description;
        public int MaxTalentLevel => maxTalentLevel;
        public int SingleLevelWeight => singleLevelWeight;
        public Sprite Icon => icon;
        
        public List<TalentLink> Links => links;

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
    }
}