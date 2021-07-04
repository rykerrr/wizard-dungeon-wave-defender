using UnityEngine;

namespace WizardGame.Stats_System
{
    [CreateAssetMenu(fileName = "Base Stat Type", menuName = "Stats/Default Base Stat")]
        public class BaseStatType : StatType
        {
            [SerializeField] private new string name = default;
            [SerializeField] private int value = default;

            public override string Name => name;
            public override int Value => value;

            public override string ToString()
            {
                return $"Default Stat Type Name: {Name} | Default Stat Type Value: {Value}";
            }
        }
}
