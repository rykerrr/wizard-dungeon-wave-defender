using System;
using UnityEngine;
using WizardGame.Combat_System.Element_System.Status_Effects;
using WizardGame.Combat_System.Spell_Effects;
using WizardGame.Health_System;
using WizardGame.ObjectRemovalHandling;

namespace WizardGame.Combat_System
{
    public class SpellEnergyBolt : SpellBase, IDamagingSpell
    {
        [Header("Properties, do not change in prefab variants")] 
        [SerializeField] private ExplosionGenerator explGenerator;
        [SerializeField] private float avgExplosionRadius = default;

        [SerializeField] private int maxExplosionTargets = default;
        [SerializeField] private int avgImpactDmg = default;
        [SerializeField] private int avgExplosionDmg = default;

        private HealthSystemBehaviour objHit = default;
        private ITimedRemovalProcessor timedRemovalProcessor;
        private Collider[] colliderHits;
        
        private Vector3 hitPos = default;

        private int actualImpactDmg = default;

        private void Awake()
        {
            timedRemovalProcessor = GetComponent<ITimedRemovalProcessor>();
        }

        public void InitSpell(float explosionRadius, float impactRadius, float explosionDmgMult
            , float impactDmgMult, Vector3 hitPos, HealthSystemBehaviour objHit, GameObject caster)
        {
            actualImpactDmg = (int)Math.Round(avgImpactDmg * impactDmgMult);
            var actualExplosionDmg = (int)Math.Round(avgExplosionDmg * explosionDmgMult);
            var actualExplosionRadius = avgExplosionRadius * explosionRadius;

            colliderHits = new Collider[maxExplosionTargets];

            this.objHit = objHit;
            this.caster = caster;
            this.hitPos = hitPos;
            
            explGenerator.Init(caster, spellElement, actualExplosionRadius, actualExplosionDmg
            , colliderHits);
            ProcessOnHitEffect();
        }

        public void ProcessOnHitEffect()
        {
            explGenerator.GenerateAndProcessExplosion(hitPos);
            
            DealDamageToImpactTarget();

            timedRemovalProcessor.Remove(0.5f);
        }

        private void DealDamageToImpactTarget()
        {
            if (ReferenceEquals(objHit, null)) return;
            
            IDamageable hitImpactTarget = default;
            
            var targExists = (hitImpactTarget = objHit.GetComponent<IDamageable>()) != null;
            if (targExists)
            {
                objHit.TakeDamage(actualImpactDmg, SpellElement, caster);
            }

            if (targExists)
            {
                TryApplyStatusEffect(hitImpactTarget);

                hitImpactTarget.TakeDamage(actualImpactDmg, SpellElement, caster);
            }
        }

        private void TryApplyStatusEffect(IDamageable hitImpactTarget)
        {
            var healthBehav = hitImpactTarget as HealthSystemBehaviour;
            if (healthBehav == null) return;
            
            var statEffData = SpellElement.StatusEffectToApply;
            var statEff = StatusEffectFactory.CreateStatusEffect(statEffData,
                caster, SpellElement, healthBehav.gameObject);

            // Delegate this over to HealthSystemBehaviour
            var statEffHandler = healthBehav.StatusEffectHandler;
            
            var res = statEffHandler.AddStatusEffect(statEffData, statEff
                , statEffData.Duration, out var buff);

            if (res == StatusEffectAddResult.SpellBuff)
            {
                actualImpactDmg = (int)Math.Round(actualImpactDmg * buff.Effectiveness);
            }
        }
    }
}
