using System.Collections.Generic;

namespace WizardGame.Item_System
{
    public class ItemComparer : IEqualityComparer<ItemSlot>
    {
        public bool Equals(ItemSlot x, ItemSlot y)
        {
            if (ReferenceEquals(null, x.invItem) || ReferenceEquals(null, y.invItem)) return false;
            
            return x.invItem == y.invItem;
        }

        public int GetHashCode(ItemSlot obj)
        {
            return base.GetHashCode();
        }
    }
}