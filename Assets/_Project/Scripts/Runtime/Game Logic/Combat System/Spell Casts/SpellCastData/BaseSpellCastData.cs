
// Whole purpose of this data and all its inheritors is to be a container for
// data relating to a SPECIFIC spell
// Element data should be part of the spell book 

namespace WizardGame.Combat_System
{
    public abstract class BaseSpellCastData
    {
        private int manaCost;
        protected bool manaCostIsDirty = true;
        
        public int ManaCost
        {
            get
            {
                if (manaCostIsDirty)
                {
                    manaCost = CalculateManaCost();
                    manaCostIsDirty = false;
                }

                return manaCost;
            }
        }

        protected abstract int CalculateManaCost();
    }
}