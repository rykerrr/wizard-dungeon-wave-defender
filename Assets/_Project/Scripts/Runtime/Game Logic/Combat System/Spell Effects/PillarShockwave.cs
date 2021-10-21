using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Combat_System.Element_System;
using WizardGame.Combat_System.Element_System.Status_Effects;
using WizardGame.Health_System;

namespace WizardGame.Combat_System.Spell_Effects
{
    public class PillarShockwave : MonoBehaviour
    {
        [SerializeField] private Renderer graphics;
        
        private GameObject caster = default;
        private Element spellElement;
        
        private Collider[] colliderHits = default;

        private Vector3 swExtents;

        private int actualSwDamage = default;

        public void Init(int actualSwDamage, Vector3 swExtents, Element spellElement, GameObject caster
            , ref Collider[] colliderHits)
        {
            this.colliderHits = colliderHits;

            this.actualSwDamage = actualSwDamage;
            this.caster = caster;
            this.swExtents = swExtents;
            this.spellElement = spellElement;

            Material graphMat = graphics.material;

            Color swColor = spellElement.ElementColor;
            graphMat.color = new Color(swColor.r, swColor.g, swColor.b, graphMat.color.a);
            
            ProcessShockwave();
        }

        private void ProcessShockwave()
        {
            var healthSysBehavs = GetHealthSystemsInBoxIgnoreCaster();

            foreach (var healthSysBehav in healthSysBehavs)
            {
                var dmg = TryApplyStatusEffect(healthSysBehav);
                
                healthSysBehav.HealthSystem.TakeDamage(dmg, spellElement, caster);
            }
        }

        // called by animation event
        public void DisableSelf()
        {
            Destroy(gameObject, 1f);
        }

        private List<HealthSystemBehaviour> GetHealthSystemsInBoxIgnoreCaster()
        {
            Array.Clear(colliderHits, 0, colliderHits.Length);

            var shockwaveHits = Physics.OverlapBoxNonAlloc(transform.position, swExtents, colliderHits,
                Quaternion.identity,
                ~0, QueryTriggerInteraction.Ignore);
            // hit every layer but ignore triggers

            var healthSystemBehaviours = GetHealthSystemsFromColliders(shockwaveHits);

            return healthSystemBehaviours;
        }

        private List<HealthSystemBehaviour> GetHealthSystemsFromColliders(int shockwaveHits)
        {
            List<HealthSystemBehaviour> healthSystemBehaviours = new List<HealthSystemBehaviour>();

            for (var i = shockwaveHits - 1; i >= 0; i--)
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
        
        private int TryApplyStatusEffect(HealthSystemBehaviour hitImpactTarget)
        {
            int dmg = actualSwDamage;
            
            var statEffData = spellElement.StatusEffectToApply;
            var statEff = StatusEffectFactory.CreateStatusEffect(statEffData,
                caster, spellElement, hitImpactTarget.gameObject);

            // Delegate this over to HealthSystemBehaviour
            var statEffHandler = hitImpactTarget.StatusEffectHandler;
            
            var res = statEffHandler.AddStatusEffect(statEffData, statEff
                , statEffData.Duration, out var buff);

            if (res == StatusEffectAddResult.SpellBuff)
            {
                dmg = (int)Math.Round(dmg * buff.Effectiveness);
            }

            return dmg;
        }
    }
}