using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Combat_System.EntityGetters;
using WizardGame.Combat_System.Spell_Effects;
using WizardGame.Health_System;
using WizardGame.Utility.Timers;

namespace WizardGame.Combat_System
{
    public class SpellDirectedEnergyExplosion : SpellBase, IDamagingSpell
    {
        [Header("References")]
        [SerializeField] private Explosion onHitEffect = default;

        [Header("Properties, do not change in prefab variants")] 
        [SerializeField] private LayerMask entitiesLayerMask;
        [SerializeField] private float avgExplosionRadius = default;
        [SerializeField] private float delayBetweenExplosions = 0.3f;
        
        [SerializeField] private int maxExplosionTargets = default;
        [SerializeField] private int avgExplosionDmg = default;
        
        private DownTimer explDelayTimer = default;
        private Collider[] colliderHits;
        
        private Vector3 explPos = default;
        
        private int actualExplosionDmg = default;
        private int explAmn = 1;
        private float actualRadius = default;

        private int ExplCount { get; set; } = 0;
        
        public void InitSpell(float explosionRadMult, float explosionDmgMult, int explAmn
            , Vector3 explPos, GameObject caster)
        {
            actualExplosionDmg = (int)Math.Round(avgExplosionDmg * explosionDmgMult);
            actualRadius = avgExplosionRadius * explosionRadMult;

            colliderHits = new Collider[maxExplosionTargets];
            
            this.explAmn = explAmn;
            this.explPos = explPos;
            
            this.caster = caster;
            
            InitTimer();
        }

        private void InitTimer()
        {
            explDelayTimer = new DownTimer(delayBetweenExplosions);

            explDelayTimer.OnTimerEnd += ProcessOnHitEffect;
            
            explDelayTimer.OnTimerEnd += () =>
            {
                ExplCount++;

                if (ExplCount > explAmn)
                {
                    explDelayTimer.OnTimerEnd = null;
                    Destroy(gameObject, 0.1f);
                }
            };
            
            explDelayTimer.OnTimerEnd += explDelayTimer.Reset;

            Debug.Log("E");
            Debug.Log(explDelayTimer.Time + " | " + explDelayTimer.DefaultTime + " | " + explDelayTimer.OnTimerEnd.Method);
        }

        private void Update()
        {
            Debug.Log(explDelayTimer);
            Debug.Log(explDelayTimer.TryTick(Time.deltaTime));
        }

        public void ProcessOnHitEffect()
        {
            var explClone = GenerateAndProcessExplosion(explPos);
            
            explClone.Init(actualExplosionDmg, actualRadius, SpellElement
                , Caster, entitiesLayerMask, ref colliderHits);
        }

        private Explosion GenerateAndProcessExplosion(Vector3 pos)
        {
            var onHitClone = Instantiate(onHitEffect, pos, Quaternion.identity);
            onHitClone.transform.localScale = Vector3.one * actualRadius;

            onHitClone.Init(actualExplosionDmg, actualRadius, SpellElement
                , Caster, entitiesLayerMask, ref colliderHits);

            return onHitClone;
        }
    }
}
