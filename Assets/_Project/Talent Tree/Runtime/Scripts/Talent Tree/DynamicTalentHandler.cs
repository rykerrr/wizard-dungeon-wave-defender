using System.Linq;
using System.Collections.Generic;
using System.Text;
using Talent_Tree.UI;
using UnityEngine;

namespace Talent_Tree.Dynamic_Talent_Tree
{
    public class DynamicTalentHandler : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Transform talentContainer = default;

        [Header("Properties")] [SerializeField]
        private int points = 20;

        private readonly List<DynamicTalentUI> talents = new List<DynamicTalentUI>();
        private StringBuilder sb = new StringBuilder();
        
        private void Start()
        {
            var talentList = GetTalentUIsFromTalentsContainer();
            
            talents.AddRange(talentList);
            sb ??= new StringBuilder();
            
            Debug.Log(talents + " | " + talents.Count);
        }

        private List<DynamicTalentUI> GetTalentUIsFromTalentsContainer()
        {
            var query = from Transform talentTransf in talentContainer
                select talentTransf.GetComponent<DynamicTalentUI>();

            return query.ToList();
        }

        public void TryLevelupTalent(DynamicTalentUI talentUi)
        {
            Debug.Log($"Talent levelup called on {talentUi}");
            
            var leveledUp = talentUi.TryLevelUp(points);

            if (leveledUp)
            {
                points -= talentUi.TalentContainer.Talent.SingleLevelWeight;
            }

            Debug.Log($"Leveled up: {leveledUp} Level weight: {talentUi.TalentContainer.Talent.SingleLevelWeight} Points: {points}");
        }

        [ContextMenu("Log all runtime-loaded talents")]
        public void LogTalentList()
        {
            sb.Clear();

            foreach (var talentUi in talents)
            {
                sb.Append(talentUi.TalentContainer.GetTalentContainerInfo()).AppendLine();
            }
            
            Debug.Log(sb.ToString());
        }
    }
}