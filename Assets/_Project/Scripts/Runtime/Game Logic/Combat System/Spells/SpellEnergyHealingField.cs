using System;
using UnityEngine;
using WizardGame.Combat_System.EntityGetters;
using WizardGame.Health_System;
using WizardGame.ObjectRemovalHandling;
using WizardGame.Utility.Timers;
using Random = System.Random;

namespace WizardGame.Combat_System
{
    public class SpellEnergyHealingField : SpellBase, IHealingSpell
    {
        [Header("References")]
        [SerializeField] private Transform healFieldGraphic = default;
        [SerializeField] private ParticleSystem healFieldEffect = default;

        [Header("Properties, do not change in prefab variants")] 
        [SerializeField] private LayerMask entitiesLayerMask;
        [SerializeField] private Vector3 fieldSizeHalfExtents = default;
        [SerializeField] private Vector3 fieldCenter = default;
        [SerializeField] private int amnOfTicks = default;
        [SerializeField] private int avgHealPerTick = default;
        [SerializeField] private int particleAmnMult = 10;
        [SerializeField] private int maxHealTargets = 20;
        [SerializeField] private float delayBetweenHealWaves = default;

        private ITimedRemovalProcessor timedRemovalProcessor;
        private DownTimer healWaveTimer = default;
        private Collider[] colliderHits = default;
        
        private int actualHealPerTick = default;
        
        public int CurrentTickCount { get; private set; } = 0;
        
        private GetEntitiesInBox<IHealable> boxEntitiesGetter;
        private GetEntitiesWithoutCaster<IHealable> noCasterEntitiesExtractor;

        private void Awake()
        {
            timedRemovalProcessor = GetComponent<ITimedRemovalProcessor>();
        }

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
            
            colliderHits = new Collider[maxHealTargets];

            boxEntitiesGetter =
                new GetEntitiesInBox<IHealable>(entitiesLayerMask, fieldCenter.HasValue ? fieldCenter.Value : transform.position, 
                    fieldSizeHalfExtents.HasValue ? fieldSizeHalfExtents.Value : transform.position);
            noCasterEntitiesExtractor = new GetEntitiesWithoutCaster<IHealable>();
            
            InitTimer();
        }

        private void InitTimer()
        {
            healWaveTimer = new DownTimer(delayBetweenHealWaves);
            
            healWaveTimer.OnTimerEnd += ApplyHeal;
            
            healWaveTimer.OnTimerEnd += () =>
            {
                CurrentTickCount++;

                if (CurrentTickCount > amnOfTicks)
                {
                    healWaveTimer.OnTimerEnd = null;
                    timedRemovalProcessor.Remove(2f);
                }
            };
            
            healWaveTimer.OnTimerEnd += healWaveTimer.Reset;
        }

        private void Update()
        {
            healWaveTimer?.TryTick(Time.deltaTime);
        }

        public void ApplyHeal()
        {
            var healthSysBehavs = noCasterEntitiesExtractor.GetTsWithoutCaster(boxEntitiesGetter, caster, ref colliderHits);

            healthSysBehavs.ForEach(x => x.Heal(actualHealPerTick, caster));

            var rand = new Random();
            var particleAmn = rand.Next(1, healthSysBehavs.Count + 1) * particleAmnMult;

            healFieldEffect.Emit(particleAmn);
        }
    }
}
