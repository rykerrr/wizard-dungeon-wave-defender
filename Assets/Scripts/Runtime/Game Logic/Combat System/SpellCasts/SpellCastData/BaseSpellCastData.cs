using System;
using System.Text;
using UnityEngine;

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