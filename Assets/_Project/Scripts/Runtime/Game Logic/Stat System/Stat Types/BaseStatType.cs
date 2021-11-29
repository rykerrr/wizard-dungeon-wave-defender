using UnityEngine;

namespace WizardGame.Stats_System
{
    [CreateAssetMenu(fileName = "New Base Stat Type", menuName = "Stats/Default Base Stat")]
        public class BaseStatType : StatType
        {
            [SerializeField] private int value = default;
            
            public override int Value => value;
        
            public override string ToString()
            {
                return $"Default Stat Type Name: {Name} | Default Stat Type Value: {Value}";
            }
        }
}
