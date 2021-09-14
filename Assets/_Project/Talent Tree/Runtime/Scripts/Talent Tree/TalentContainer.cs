using System;
using UnityEngine;

namespace Talent_Tree
{
    [Serializable]
    public class TalentContainer
    {
        [SerializeField] private Talent talent = default;
        
        public Talent Talent => talent;

        private int currentTalentLevel = 0;

        public int CurrentTalentLevel => currentTalentLevel;

        public TalentContainer(Talent talent)
        {
            this.talent = talent;
        }
        
        public bool TryLevelupTalent(int points)
        {
            var notEnoughPoints = points < talent.SingleLevelWeight;
            var levelTooHigh = currentTalentLevel >= talent.MaxTalentLevel;

            if (notEnoughPoints || levelTooHigh) return false;

            currentTalentLevel++;
            
            return true;
        }

        public string GetTalentContainerInfo()
        {
            return $"[\nName: {talent.Name}\nCurrent talent level: {currentTalentLevel}\n" +
                   $"Max talent level: {talent.MaxTalentLevel}\nDescription: {talent.Description}\n]";
        }
    }
}
