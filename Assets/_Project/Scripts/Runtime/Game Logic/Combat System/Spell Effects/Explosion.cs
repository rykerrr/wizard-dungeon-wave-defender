using System;
using System.Collections.Generic;
using Ludiq;
using UnityEngine;
using WizardGame.Combat_System.EntityGetters;
using WizardGame.Combat_System.Element_System;
using WizardGame.Combat_System.Element_System.Status_Effects;
using WizardGame.Health_System;

namespace WizardGame.Combat_System.Spell_Effects
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private FadeMeshRendererOverLifetime fader = default;
        
        private GetEntitiesInRadius<IDamageable> radiusEntitiesGetter = default;
        private GetEntitiesWithoutCaster<IDamageable> noCasterEntitiesExtractor = default;
        
        private GameObject caster;
        private Element spellElement;

        private Collider[] colliderHits;
        private float explosionRadius;
        private int explosionDamage;

        // Object's lifetime is handled by it's animation currently
        public void Init(int explosionDamage, float explosionRadius, Element spellElement, GameObject caster
            , LayerMask entitiesLayerMask, ref Collider[] colliderHits)
        {
            this.explosionDamage = explosionDamage;
            this.explosionRadius = explosionRadius;
            this.caster = caster;
            this.colliderHits = colliderHits;

            this.spellElement = spellElement;
            fader.CustomColor = spellElement.ElementColor;
            
            noCasterEntitiesExtractor = new GetEntitiesWithoutCaster<IDamageable>();
            radiusEntitiesGetter = new GetEntitiesInRadius<IDamageable>(entitiesLayerMask, transform.position, this.explosionRadius);

            ProcessExplosion();
        }

        protected virtual void ProcessExplosion()
        {
            var damageables = noCasterEntitiesExtractor
                .GetTsWithoutCaster(radiusEntitiesGetter, caster, ref colliderHits);

            foreach (var damageable in damageables)
            {
                TryApplyStatusEffect(damageable);

                damageable.TakeDamage(explosionDamage, spellElement, caster);
            }
        }
        
        private void TryApplyStatusEffect(IDamageable hitImpactTarget)
        {
            var healthBehav = hitImpactTarget as HealthSystemBehaviour;
            if (healthBehav == null) return;
            
            var statEffData = spellElement.StatusEffectToApply;
            var statEff = StatusEffectFactory.CreateStatusEffect(statEffData,
                caster, spellElement, healthBehav.gameObject);

            // Delegate this over to HealthSystemBehaviour
            var statEffHandler = healthBehav.StatusEffectHandler;
            
            var res = statEffHandler.AddStatusEffect(statEffData, statEff
                , statEffData.Duration, out var buff);

            if (res == StatusEffectAddResult.SpellBuff)
            {
                explosionDamage = (int)Math.Round(explosionDamage * buff.Effectiveness);
            }
        }

        // called by animation event
        public void DisableSelf()
        {
            Destroy(gameObject, 1f);
        }
    }
}