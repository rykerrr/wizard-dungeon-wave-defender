using System;
using UnityEngine;

namespace WizardGame.Level_System
{
    [Serializable]
    public class LevelSystem
    {
        public event Action onLevelUpEvent = delegate { };
        
        private int curLevel = 0;
        private int requiredExp = 0;
        private int curExp = 0;

        public float LevelExpPercentage => (float) Math.Round((float) curExp / requiredExp, 3) * 100f;
        public int CurLevel => curLevel;
        public int CurExp => curExp;
        public int RequiredExp => requiredExp;
        private bool LevelUpAvailable => curExp >= requiredExp;

        public LevelSystem(int curLevel)
        {
            Init(curLevel);
        }

        public void Init(int curLevel)
        {
            curExp = CalculateTotalExpRequiredForLevel(curLevel);

            TryLevelUp();
            requiredExp = CalculateRequiredExp();
        }

        public void AddExp(int expToAdd)
        {
            curExp += expToAdd;

            if (LevelUpAvailable) TryLevelUp();
        }

        private void TryLevelUp()
        {
            while (LevelUpAvailable)
            {
                curExp -= requiredExp;
                curLevel++;

                requiredExp = CalculateRequiredExp();

                onLevelUpEvent?.Invoke();
            }
        }

        private int CalculateRequiredExp()
        {
            return Mathf.RoundToInt(0.04f * Mathf.Pow(curLevel, 3) + 0.8f * curLevel
                                                                   + 2 * curLevel + 1);
        }

        private static int CalculateTotalExpRequiredForLevel(int level)
        {
            var expSum = 0;

            for (var i = 0; i < level; i++)
            {
                expSum += Mathf.RoundToInt(0.04f * Mathf.Pow(i, 3) + 0.8f * i
                                                                   + 2 * i + 1);
            }

            return expSum;
        }

        public static int CalculateLifeExperienceValue(int level, float entityLifeValueMultiplier)
        {
            return Mathf.RoundToInt(level * entityLifeValueMultiplier);
        }

        public override string ToString()
        {
            return
                $"Level: {CurLevel} | Exp/Required Exp: {CurExp}/{RequiredExp} | Exp/ReqExp Percentage Wise: {LevelExpPercentage}%";
        }
    }
}