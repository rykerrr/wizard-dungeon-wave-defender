using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Health_System;
using WizardGame.Utility.Timers;

namespace WizardGame.Combat_System
{
    public class SpellDirectedEnergyExplosion : MonoBehaviour, IDamagingSpell
    {
        [Header("References")]
        [SerializeField] private GameObject explosionEffect = default;

        [Header("Properties")]
        [SerializeField] private float avgExplosionRadius = default;
        [SerializeField] private float delayBetweenExplosions = 0.3f;
        
        [SerializeField] private int maxExplosionTargets = default;
        [SerializeField] private int avgExplosionDmg = default;

        private GameObject caster = default;

        private DownTimer explDelayTimer = default;
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

            this.explAmn = explAmn;
            this.explPos = explPos;
            
            this.caster = caster;

            InitTimer();
        }

        private void InitTimer()
        {
            explDelayTimer = new DownTimer(delayBetweenExplosions);

            explDelayTimer.OnTimerEnd += explDelayTimer.Reset;
            explDelayTimer.OnTimerEnd += ProcessOnHit;
            explDelayTimer.OnTimerEnd += () =>
            {
                ExplCount++;
                ProcessOnHit();

                if (ExplCount > explAmn)
                {
                    explDelayTimer.OnTimerEnd = null;
                    Destroy(gameObject, 0.1f);
                }
            };
        }

        private void Update()
        {
            explDelayTimer?.TryTick(Time.deltaTime);
        }

        public void ProcessOnHit()
        {
            var explClone = GenerateAndProcessExplosion(explPos);
            
            Destroy(explClone, 0.2f);
        }

        private GameObject GenerateAndProcessExplosion(Vector3 pos)
        {
            var explClone = Instantiate(explosionEffect, pos, Quaternion.identity);
            explClone.transform.localScale = Vector3.one * avgExplosionRadius;

            var healthSystemBehaviours = GetHealthSystemsInRadiusIgnoreCaster();
            healthSystemBehaviours.ForEach(x => x.HealthSystem.TakeDamage(actualExplosionDmg, caster));

            return explClone;
        }

        private List<HealthSystemBehaviour> GetHealthSystemsInRadiusIgnoreCaster()
        {
            var colliderHits = new Collider[maxExplosionTargets];
            var explosionHits = Physics.OverlapSphereNonAlloc(transform.position, actualRadius, colliderHits);
            var healthSystemBehaviours = new List<HealthSystemBehaviour>();

            for (var i = explosionHits - 1; i >= 0; i--)
            {
                if (colliderHits[i].gameObject == caster)
                {
                    continue;
                }
                
                HealthSystemBehaviour behav = default;

                if (!ReferenceEquals(behav = colliderHits[i].GetComponent<HealthSystemBehaviour>(), null))
                {
                    healthSystemBehaviours.Add(behav);
                }
            }

            return healthSystemBehaviours;
        }
    }
}
