using System.Linq;
using System.Collections.Generic;
using System.Text;
using Talent_Tree.UI;
using UnityEngine;

namespace Talent_Tree
{
    public class TalentHandler : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private Transform talentContainer = default;
        [SerializeField] private TalentLevelUpActionInjector injector = default;
        
        [Header("Properties")] [SerializeField]
        private int points = 20;

        private readonly Dictionary<Talent, DynamicTalentUI> talents =
            new Dictionary<Talent, DynamicTalentUI>();
        
        private StringBuilder sb = new StringBuilder();

        public IReadOnlyDictionary<Talent, DynamicTalentUI> Talents => talents;
        
        private void Start()
        {
            GetTalentUIsFromTalentsContainer();
            
            injector.InjectDependencies();
            
            sb ??= new StringBuilder();
        }

        private void GetTalentUIsFromTalentsContainer()
        {
            var query = from Transform talentUi in talentContainer
                where talentUi.GetComponent<DynamicTalentUI>() let src = talentUi.GetComponent<DynamicTalentUI>()
                select new KeyValuePair<Talent, DynamicTalentUI>(src.TalentContainer.Talent, src);

            foreach (var kvp in query)
            {
                talents.Add(kvp.Key, kvp.Value);
            }
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
                var talContainer = talentUi.Value.TalentContainer;
                
                sb.Append(talContainer.GetTalentContainerInfo()).AppendLine();
            }
            
            Debug.Log(sb.ToString());
        }
    }
}