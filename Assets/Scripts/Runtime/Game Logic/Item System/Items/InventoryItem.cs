using System.Text;
using UnityEngine;

namespace WizardGame.Item_System.Items
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

        public void Init(Rarity rarity, int sellPrice, int maxStack)
        {
            Debug.Log("inventory innit blimey mate");
            
            this.rarity = rarity;
            this.sellPrice = sellPrice;
            this.maxStack = maxStack;
        }
        
        public InventoryItem() : base() { }
        
        public abstract override string GetInfoDisplayText();

        public override string ToString()
        {
            base.ToString();

            sb.Append("Inventory Item | Rarity: ").Append(Rarity).Append(", Sell Price: ").Append(SellPrice)
                .Append(", Max Stack: ").Append(MaxStack).AppendLine();

            return sb.ToString();
        }
    }
}