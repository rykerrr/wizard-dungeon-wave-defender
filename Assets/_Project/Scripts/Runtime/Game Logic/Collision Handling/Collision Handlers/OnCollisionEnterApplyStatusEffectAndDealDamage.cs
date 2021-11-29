using System;
using UnityEngine;
using WizardGame.Combat_System.Element_System;
using WizardGame.Combat_System.Element_System.Status_Effects;
using WizardGame.Health_System;

namespace WizardGame.CollisionHandling
{
    public class OnCollisionEnterApplyStatusEffectAndDealDamage : MonoBehaviour, ICollisionHandler
    {
        private Element element;
        private GameObject owner;
        private int dmg;

        public void Init(Element element, GameObject owner, int dmg)
        {
            this.dmg = dmg;
            this.element = element;
            this.owner = owner;
        }
        
        public void ProcessCollision(GameObject other, CollisionType type)
        {
            if (type != CollisionType.CollisionEnter && type != CollisionType.TriggerEnter) return;

            var targetExists = TryGetComponent<IDamageable>(out var hit);
            if (!targetExists) return;
            
            DealDamageToHitTarget(hit);
        }

        private void DealDamageToHitTarget(IDamageable hit)
        {
            TryApplyStatusEffect(hit);

            hit.TakeDamage(dmg, element, owner);
        }

        private void TryApplyStatusEffect(IDamageable hitImpactTarget)
        {
            var healthBehav = hitImpactTarget as HealthSystemBehaviour;
            if (healthBehav == null) return;

            var statEffData = element.StatusEffectToApply;
            var statEff = StatusEffectFactory.CreateStatusEffect(statEffData,
                owner, element, healthBehav.gameObject);

            // Delegate this over to HealthSystemBehaviour
            var statEffHandler = healthBehav.StatusEffectHandler;

            var res = statEffHandler.AddStatusEffect(statEffData, statEff
                , statEffData.Duration, out var buff);

            if (res == StatusEffectAddResult.SpellBuff)
            {
                dmg = (int) Math.Round(dmg * buff.Effectiveness);
            }
        }
    }
}