using System;
using System.Collections.Generic;
using Ludiq;
using UnityEngine;
using WizardGame.Combat_System.EntityGetters;
using WizardGame.Combat_System.Element_System;
using WizardGame.Combat_System.Element_System.Status_Effects;
using WizardGame.Health_System;
using WizardGame.ObjectRemovalHandling;

namespace WizardGame.Combat_System.Spell_Effects
{
    public class PillarShockwave : MonoBehaviour
    {
        [SerializeField] private Renderer graphics;

        private GetEntitiesInBox<IDamageable> boxEntitiesGetter = default;
        private GetEntitiesWithoutCaster<IDamageable> noCasterEntitiesExtractor = default;
        private IRemovalProcessor removalProcessor;
        
        private GameObject caster = default;
        private Element spellElement;
        
        private Collider[] colliderHits = default;

        private Vector3 swExtents;

        private int actualSwDamage = default;

        private void Awake()
        {
            removalProcessor = GetComponent<IRemovalProcessor>();
        }

        public void Init(int actualSwDamage, Vector3 swExtents, Element spellElement, GameObject caster,
            LayerMask entitiesLayerMask, ref Collider[] colliderHits)
        {
            this.colliderHits = colliderHits;

            this.actualSwDamage = actualSwDamage;
            this.caster = caster;
            this.swExtents = swExtents;
            this.spellElement = spellElement;

            Material graphMat = graphics.material;

            Color swColor = spellElement.ElementColor;
            graphMat.color = new Color(swColor.r, swColor.g, swColor.b, graphMat.color.a);

            noCasterEntitiesExtractor = new GetEntitiesWithoutCaster<IDamageable>();
            boxEntitiesGetter = new GetEntitiesInBox<IDamageable>(entitiesLayerMask, transform.position, swExtents);

            ProcessShockwave();
        }

        private void ProcessShockwave()
        {
            var damageables = noCasterEntitiesExtractor
                .GetTsWithoutCaster(boxEntitiesGetter, caster, ref colliderHits);

            foreach (var damageable in damageables)
            {
                TryApplyStatusEffect(damageable);

                damageable.TakeDamage(actualSwDamage, spellElement, caster);
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
                actualSwDamage = (int)Math.Round(actualSwDamage * buff.Effectiveness);
            }
        }

        // called by animation event
        public void DisableSelf()
        {
            removalProcessor.Remove();
        }
    }
}