using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Combat_System.Element_System;
using WizardGame.Combat_System.Element_System.Status_Effects;
using WizardGame.Health_System;

namespace WizardGame.Combat_System.Spell_Effects
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private FadeMeshRendererOverLifetime fader = default;

        private GameObject caster;
        private Element spellElement;

        private Collider[] colliderHits;
        private float explosionRadius;
        private int explosionDamage;

        // Object's lifetime is handled by it's animation currently
        public void Init(int explosionDamage, float explosionRadius, Element spellElement, GameObject caster
            , ref Collider[] colliderHits)
        {
            this.explosionDamage = explosionDamage;
            this.explosionRadius = explosionRadius;
            this.caster = caster;
            this.colliderHits = colliderHits;

            this.spellElement = spellElement;
            fader.CustomColor = spellElement.ElementColor;

            ProcessExplosion();
        }

        protected virtual void ProcessExplosion()
        {
            var healthSystemBehaviours = GetHealthSystemsInRadiusIgnoreCaster();

            foreach (var healthObj in healthSystemBehaviours)
            {
                var dmg = TryApplyStatusEffect(healthObj);

                healthObj.HealthSystem.TakeDamage(dmg, spellElement, caster);
            }
        }

        private int TryApplyStatusEffect(HealthSystemBehaviour hitImpactTarget)
        {
            var statEffData = spellElement.StatusEffectToApply;
            
            var statEff = StatusEffectFactory.CreateStatusEffect(statEffData
                , caster, spellElement, hitImpactTarget.gameObject);

            var statEffHandler = hitImpactTarget.StatusEffectHandler;
            
            var res = statEffHandler.AddStatusEffect(statEffData, statEff
                , statEffData.Duration, out var buff);

            if (res == StatusEffectAddResult.SpellBuff)
            {
                return (int) Math.Round(explosionDamage * buff.Effectiveness);
            }

            return explosionDamage;
        }

        // called by animation event
        public void DisableSelf()
        {
            Destroy(gameObject, 1f);
        }

        private List<HealthSystemBehaviour> GetHealthSystemsInRadiusIgnoreCaster()
        {
            Array.Clear(colliderHits, 0, colliderHits.Length);

            var explosionHits = Physics.OverlapSphereNonAlloc(transform.position, explosionRadius,
                colliderHits, ~0, QueryTriggerInteraction.Ignore);
            // hit every layer but ignore triggers

            var healthSystemBehaviours = ReturnHealthSystemsFromColliders(explosionHits);

            return healthSystemBehaviours;
        }

        private List<HealthSystemBehaviour> ReturnHealthSystemsFromColliders(int explosionHits)
        {
            List<HealthSystemBehaviour> healthSystemBehaviours = new List<HealthSystemBehaviour>();

            for (var i = explosionHits - 1; i >= 0; i--)
            {
                HealthSystemBehaviour behav = default;

                if (colliderHits[i].attachedRigidbody)
                {
                    if ((behav = colliderHits[i].attachedRigidbody.GetComponent<HealthSystemBehaviour>()) != null)
                    {
                        if (healthSystemBehaviours.Contains(behav) || behav.gameObject == caster) continue;
                        
                        healthSystemBehaviours.Add(behav);
                        continue;
                    }
                }
                
                if ((behav = colliderHits[i].GetComponent<HealthSystemBehaviour>()) != null)
                {
                    if (healthSystemBehaviours.Contains(behav) || behav.gameObject == caster) continue;
                    healthSystemBehaviours.Add(behav);
                }
            }

            return healthSystemBehaviours;
        }
    }
}