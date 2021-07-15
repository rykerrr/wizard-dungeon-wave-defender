using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Health_System;
using WizardGame.Utility.Timers;
using Random = System.Random;

namespace WizardGame.Combat_System
{
    public class SpellEnergyHealingField : MonoBehaviour, IHealingSpell
    {
        [Header("References")]
        [SerializeField] private Transform healFieldGraphic = default;
        [SerializeField] private ParticleSystem healFieldEffect = default;

        [Header("Properties")]
        [SerializeField] private Vector3 fieldSizeHalfExtents = default;
        [SerializeField] private Vector3 fieldCenter = default;
        [SerializeField] private int amnOfTicks = default;
        [SerializeField] private int avgHealPerTick = default;
        [SerializeField] private int particleAmnMult = 10;
        [SerializeField] private float delayBetweenHealWaves = default;

        private DownTimer healWaveTimer = default;
        private GameObject caster = default;

        private int actualHealPerTick = default;
        
        public int CurrentTickCount { get; private set; } = 0;
        
        public void InitSpell(float healPerTickMult, int amnOfTicks, GameObject caster, Vector3? fieldCenter = null, Vector3? fieldSizeHalfExtents = null)
        {
            this.caster = caster;
            this.amnOfTicks = amnOfTicks;
            
            if(fieldCenter.HasValue) this.fieldCenter = fieldCenter.Value;
            if(fieldSizeHalfExtents.HasValue) this.fieldSizeHalfExtents = fieldSizeHalfExtents.Value;

            healFieldGraphic.localScale = this.fieldSizeHalfExtents * 2;

            var fieldEffectShape = healFieldEffect.shape;
            fieldEffectShape.scale = new Vector2(this.fieldSizeHalfExtents.x, this.fieldSizeHalfExtents.z);

            actualHealPerTick = (int)Math.Round(avgHealPerTick * healPerTickMult);
            
            InitTimer();
        }

        private void InitTimer()
        {
            healWaveTimer = new DownTimer(delayBetweenHealWaves);
            healWaveTimer.OnTimerEnd += ApplyHeal;
            healWaveTimer.OnTimerEnd += healWaveTimer.Reset;

            healWaveTimer.OnTimerEnd += () =>
            {
                CurrentTickCount++;

                if (CurrentTickCount > amnOfTicks)
                {
                    healWaveTimer.OnTimerEnd = null;
                    Destroy(gameObject, 2f);
                }
            };
        }

        private void Update()
        {
            healWaveTimer?.TryTick(Time.deltaTime);
        }

        public void ApplyHeal()
        {
            var healthSysBehavs = GetHealthSystemsInBox();
            
            healthSysBehavs.ForEach(x => x.HealthSystem.Heal(actualHealPerTick, caster));

            Random rand = new Random();
            int particleAmn = rand.Next(1, healthSysBehavs.Count + 1) * particleAmnMult;
            
            healFieldEffect.Emit(particleAmn);
        }

        private List<HealthSystemBehaviour> GetHealthSystemsInBox()
        {
            var colliders = Physics.OverlapBox(fieldCenter, fieldSizeHalfExtents, Quaternion.identity, ~0,
                QueryTriggerInteraction.Ignore);

            var healthSystemBehaviours = new List<HealthSystemBehaviour>();

            for (var i = colliders.Length - 1; i >= 0; i--)
            {
                HealthSystemBehaviour behav = default;

                if (!ReferenceEquals(behav = colliders[i].GetComponent<HealthSystemBehaviour>(), null))
                {
                    healthSystemBehaviours.Add(behav);
                }
            }

            return healthSystemBehaviours;
        }
    }
}
