using System.Collections.Generic;
using Ludiq;
using UnityEngine;
using WizardGame.Movement.Position;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class StunnedStatusEffect : StatusEffectBase
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
            // Perhaps filled with graphical data eventually
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