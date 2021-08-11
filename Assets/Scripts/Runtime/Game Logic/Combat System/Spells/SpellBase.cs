using UnityEngine;
using WizardGame.Combat_System.Element_System;

namespace WizardGame.Combat_System
{
    public abstract class SpellBase : MonoBehaviour
    {
        // Set for spells that ARE of an element, shown for debug
        [SerializeField] protected Element spellElement;
        
        protected GameObject caster;

        public Element SpellElement => spellElement;
        public GameObject Caster => caster;
    }
}