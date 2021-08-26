using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class FrozenStatusEffect : StatusEffect
    {
        private readonly List<MonoBehaviour> monoBehavs = new List<MonoBehaviour>();
        
        public override void Init(GameObject caster, GameObject target, Element element, StatusEffectData data)
        {
            Debug.Log("Stunned newb");
            
            base.Init(caster, target, element, data);
            
            monoBehavs.Add(target.GetComponent<SpellCastHandler>());

            SetMonoBehavsEnabled(false);
        }
        
        public override void Tick()
        {
            Debug.Log("frozen");
        }

        public override void OnRemove()
        {
            SetMonoBehavsEnabled(true);
        }

        private void SetMonoBehavsEnabled(bool enabled)
        {
            foreach (var mono in monoBehavs)
            {
                mono.enabled = enabled;
            }
        }
    }
}