using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Utility.Timers;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    [Serializable]
    public abstract class BaseStatusEffect
    {
        protected GameObject caster;
        protected GameObject target;

        public GameObject Caster => caster;
        public GameObject Target => target;
        
        public virtual void Init(GameObject caster, GameObject target, ElementStatusEffectData data)
        {
            this.caster = caster;
            this.target = target;
        }

        public abstract void Tick();
        public abstract void OnRemove();
    }
}