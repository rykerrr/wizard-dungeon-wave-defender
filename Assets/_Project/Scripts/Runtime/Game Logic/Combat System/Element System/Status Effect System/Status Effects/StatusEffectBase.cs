using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Utility.Timers;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public abstract class StatusEffectBase
    {
        private string name = default;
        
        protected GameObject caster;
        protected GameObject target;
        protected StatusEffectStackType stackType;
        protected float externalMultiplier = 1;
        protected Element statEffElement = default;

        public string Name => name;
        
        public GameObject Caster => caster;
        public GameObject Target => target;
        public StatusEffectStackType StackType => stackType;

        public float MovementMultiplier { get; protected set; }
        public float ExternalMultiplier 
        { 
            get => externalMultiplier;
            set => externalMultiplier = value;
        }

        public virtual void Init(GameObject caster, GameObject target, Element element, StatusEffectData data)
        {
            this.caster = caster;
            this.target = target;

            name = data.Name;
            stackType = data.StackType;
            MovementMultiplier = data.MovementSpeedMultiplier;
        }

        public abstract void Tick();
        public abstract void OnRemove();
    }
}