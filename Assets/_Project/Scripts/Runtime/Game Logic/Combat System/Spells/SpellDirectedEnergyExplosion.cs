using System;
using UnityEngine;
using WizardGame.Combat_System.Spell_Effects;
using WizardGame.ObjectRemovalHandling;
using WizardGame.Utility.Timers;

namespace WizardGame.Combat_System
{
    public class SpellDirectedEnergyExplosion : SpellBase
    {
        [Header("Properties, do not change in prefab variants")] 
        [SerializeField] private ExplosionGenerator explGenerator;
        [SerializeField] private float avgExplosionRadius = default;
        [SerializeField] private float delayBetweenExplosions = 0.3f;
        
        [SerializeField] private int maxExplosionTargets = default;
        [SerializeField] private int avgExplosionDmg = default;

        private ITimedRemovalProcessor timedRemovalProcessor;
        private DownTimer explDelayTimer = default;
        private Collider[] colliderHits;
        
        private Vector3 explPos = default;
        
        private int explAmn = 1;

        private int ExplCount { get; set; } = 0;

        private void Awake()
        {
            timedRemovalProcessor = GetComponent<ITimedRemovalProcessor>();
        }

        public void InitSpell(float explosionRadMult, float explosionDmgMult, int explAmn
            , Vector3 explPos, GameObject caster)
        {
            var actualExplosionDmg = (int)Math.Round(avgExplosionDmg * explosionDmgMult);
            var actualRadius = avgExplosionRadius * explosionRadMult;

            colliderHits = new Collider[maxExplosionTargets];
            
            this.explAmn = explAmn;
            this.explPos = explPos;
            this.caster = caster;
            
            explGenerator.Init(caster, spellElement, actualRadius, actualExplosionDmg,
                colliderHits);
            InitTimer();
        }

        private void InitTimer()
        {
            explDelayTimer = new DownTimer(delayBetweenExplosions);

            explDelayTimer.OnTimerEnd += GenerateExplosion;
            
            explDelayTimer.OnTimerEnd += () =>
            {
                ExplCount++;

                if (ExplCount > explAmn)
                {
                    explDelayTimer.OnTimerEnd = null;
                    timedRemovalProcessor.Remove(0.1f);
                }
            };
            
            explDelayTimer.OnTimerEnd += explDelayTimer.Reset;
        }

        private void Update()
        {
            explDelayTimer.TryTick(Time.deltaTime);
        }

        private void GenerateExplosion()
        {
            explGenerator.GenerateAndProcessExplosion(explPos);
        }
    }
}
