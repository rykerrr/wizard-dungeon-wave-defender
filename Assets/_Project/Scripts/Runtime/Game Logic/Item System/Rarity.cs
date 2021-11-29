using UnityEngine;
using WizardGame.Utility;

namespace WizardGame.Item_System
{
    [CreateAssetMenu(fileName = "New Rarity", menuName = "Rarity/New Rarity")]
    public class Rarity : ScriptableObjectAutoNameSet
    {
        [SerializeField] private Color rarityColor = default;
        
        public Color RarityColor => rarityColor;
    }
}