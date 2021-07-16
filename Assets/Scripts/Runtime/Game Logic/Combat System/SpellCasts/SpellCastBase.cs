using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using WizardGame.Stats_System;
using WizardGame.Utility.Timers;

namespace WizardGame.Combat_System
{
    public abstract class SpellCastBase : MonoBehaviour
    {
        [SerializeField] private float castingSpeed = default;
        [SerializeField] private float castCooldown = default;
        
        private StatsSystemBehaviour statsSysBehaviour = default;
        protected CastPlaceholder castCircle = default;
        private List<MonoBehaviour> movementScripts = new List<MonoBehaviour>();
        
        protected StatsSystem statsSys = default;
        protected WaitForSeconds castingTimeWait = default;
        protected DownTimer castCooldownTimer = default;
        protected Animator castCircleAnimator = default;
        
        protected static int BeginCastHash = Animator.StringToHash("BeginSpellCast");
        protected static int EndCastHash = Animator.StringToHash("EndSpellCast");

        private EventSystem curEvSystem = default;
        private bool[] prevEnableStates;
        protected bool isCasting = false;

        public abstract BaseSpellCastData Data { get; set; }
        public GameObject Owner { get; private set; } = default; 
        public bool CanCast => !isCasting && castCooldownTimer.Time <= 0;
        public bool IsCasting => isCasting;
        

        // change owner param type to StatsSystemBehaviour since we REQUIRE it?
        // just init castCircle or also instantiate it here? think init fits more
        public virtual void Init(GameObject owner, CastPlaceholder castCircle
            , BaseSpellCastData data, params MonoBehaviour[] movementScripts)
        {
            Owner = owner;

            this.castCircle = castCircle;
            this.movementScripts = movementScripts.ToList();
            
            Data = data;
            
            statsSysBehaviour = owner.GetComponent<StatsSystemBehaviour>();
            castCircleAnimator = castCircle.GetComponent<Animator>();
            curEvSystem = EventSystem.current;
            
            statsSys = statsSysBehaviour.StatsSystem;
            
            prevEnableStates = new bool[movementScripts.Length];

            InitTimer();
        }

        private void InitTimer()
        {
            castingTimeWait = new WaitForSeconds(castingSpeed);
            castCooldownTimer = new DownTimer(castCooldown);
            castCooldownTimer.OnTimerEnd += castCooldownTimer.DisableTimer;
        }

        protected virtual void Update()
        {
            castCooldownTimer.TryTick(Time.deltaTime);
        }

        public void CastSpell()
        {
            if (!CanCast  || curEvSystem.IsPointerOverGameObject())
            {
#if UNITY_EDITOR
                Debug.Log($"Can't cast due to: Timer Enabled: {castCooldownTimer.IsTimerEnabled}" +
                          $" | Is casting: {isCasting} | Cooldown for cast left: {castCooldownTimer.Time}");
#endif

                return;
            }
            
            StartCoroutine(StartSpellCast());
        }

        protected abstract IEnumerator StartSpellCast();
        public abstract void FinishSpellCast();

        protected void EnableCastCooldown()
        {
            castCooldownTimer.Reset();
            castCooldownTimer.EnableTimer();
        }
        
        protected void DeactivateMovementScripts()
        {
            for (var i = 0; i < movementScripts.Count; i++)
            {
                prevEnableStates[i] = movementScripts[i].enabled;
                movementScripts[i].enabled = false;
            }
        }

        protected void ReactivateMovementScripts()
        {
            for (var i = 0; i < movementScripts.Count; i++)
            {
                movementScripts[i].enabled = prevEnableStates[i];
            }
        }
    }
}
