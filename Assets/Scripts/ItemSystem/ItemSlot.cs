using System;
using UnityEngine;

namespace WizardGame.ItemSystem
{
    [Serializable]
    public struct ItemSlot
    // is overriding .Equals() even worth it as it'll mean .GetHashCode() has to be overridden as well, although ==
    // should do the job perfectly for value types
    {
        [SerializeField] [Min(0)] private int quantity;
        public InventoryItem invItem;

        public int Quantity
        {
            get => quantity;
            set => quantity = Mathf.Max(value, 0);
        }
        public static ItemSlot Empty => new ItemSlot(null, 0);
        
        public ItemSlot(InventoryItem invItem, int quantity)
        {
            this.quantity = Mathf.Max(quantity, 0);
            this.invItem = invItem;
        }

        public ItemSlot ToEmptyQuantity() => new ItemSlot(invItem, 0);

        public override string ToString()
        {
            return $"Item: {invItem} ; Quantity: {Quantity}";
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(obj, null) && this == (ItemSlot)obj; //this == (ItemSlot) obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(ItemSlot a, ItemSlot b)
        {
            return a.invItem == b.invItem && a.Quantity == b.Quantity;
        }

        public static bool operator !=(ItemSlot a, ItemSlot b)
        {
            return !a.Equals(b);
        }
    }
}