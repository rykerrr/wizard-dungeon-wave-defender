using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Utility.Timers;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public abstract class StatusEffect
    {
        protected GameObject caster;
        protected GameObject target;
        protected StatusEffectStackType stackType;
        
        public GameObject Caster => caster;
        public GameObject Target => target;
        public StatusEffectStackType StackType => stackType;
        
        public virtual void Init(GameObject caster, GameObject target, StatusEffectData data)
        {
            this.caster = caster;
            this.target = target;

            stackType = data.StackType;
        }

        public abstract void Tick();
        public abstract void OnRemove();
    }
}