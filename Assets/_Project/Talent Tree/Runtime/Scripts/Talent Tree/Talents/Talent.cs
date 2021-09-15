using System.Collections.Generic;
using System.IO;
using System.Text;
using Ludiq;
using UnityEditor;
using UnityEngine;

namespace Talent_Tree
{
    [CreateAssetMenu(menuName = "Talents/New Talent Asset", fileName = "New Talent Asset")]
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
        
        [SerializeField] private List<TalentLevelUpAction> talentLevelUpActions = default;
        [SerializeField] private List<TalentLevelUpAction> talentUnlockActions = default;

        [Header("Talent links")] 
        [SerializeField] private List<TalentLink> links;
        public string Name => name;
        public int MaxTalentLevel => maxTalentLevel;
        public int SingleLevelWeight => singleLevelWeight;
        
        public Sprite Icon => icon;
        
        public List<TalentLevelUpAction> TalentLevelUpActions => talentLevelUpActions;
        public List<TalentLevelUpAction> TalentUnlockActions => talentUnlockActions;

        public List<TalentLink> Links => links;

        private StringBuilder sb = new StringBuilder();
        
        public string GetDescriptionText()
        {
            sb.Clear();
            
            sb.Append(description).AppendLine();

            sb.Append("On unlock actions: \n");
            foreach (var unlockAction in talentUnlockActions)
            {
                sb.Append("-  ").Append(unlockAction.ToString()).AppendLine();
            }
            
            sb.Append("Level up actions: \n");
            foreach (var levelUpAction in talentLevelUpActions)
            {
                sb.Append("-  ").Append(levelUpAction.ToString()).AppendLine();
            }

            return sb.ToString();
        }
        
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