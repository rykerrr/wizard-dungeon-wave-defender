using UnityEngine;

namespace WizardGame.ItemSystem
{
    public abstract class InventoryItem : HotbarItem
    {
        [SerializeField] private Rarity rarity = default;
        [SerializeField] private int sellPrice = 1;
        [SerializeField] private int maxStack = 5;

        public Rarity Rarity => rarity;
        public int SellPrice => sellPrice;
        public virtual int MaxStack => maxStack;
        
        public override string ColouredName
        {
            get
            {
                string colourInHex = ColorUtility.ToHtmlStringRGB(rarity.RarityColor);
                return $"<color=#{colourInHex}>{Name}</color>";
            }
        }
        
        public abstract override string GetInfoDisplayText();
    }
}