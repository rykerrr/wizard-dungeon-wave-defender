using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WizardGame.Combat_System.Mock_Up;
using WizardGame.Stats_System;
using WizardGame.Utility.Timers;

namespace WizardGame.Combat_System
{
    public abstract class SpellCastBase : MonoBehaviour
    {
        [SerializeField] private StatsSystemBehaviour statsSysBehaviour = default;
        [SerializeField] protected CastPlaceholder castCircle = default;
        
        [SerializeField] private List<MonoBehaviour> movementScripts = new List<MonoBehaviour>();

        private SpellCastData spellCastData = default;

        protected StatsSystem statsSys = default;
        protected WaitForSeconds castingTimeWait = default;
        protected DownTimer castCooldownTimer = default;

        protected Animator castCircleAnimator = default;
        
        protected static int BeginCastHash = Animator.StringToHash("BeginSpellCast");
        protected static int EndCastHash = Animator.StringToHash("EndSpellCast");

        private bool[] prevEnableStates;
        protected bool isCasting = false;

        public GameObject Owner { get; private set; } = default;
        public SpellCastData SpellCastData => spellCastData;
        public bool CanCast => !isCasting && castCooldownTimer.Time <= 0;
        public bool IsCasting => isCasting;
        

        // change owner param type to StatsSystemBehaviour since we REQUIRE it?
        // just init castCircle or also instantiate it here? think init fits more
        public virtual void Init(GameObject owner, CastPlaceholder castCircle
            , SpellCastData spellCastData, params MonoBehaviour[] movementScripts)
        {
            Owner = owner;

            this.castCircle = castCircle;
            this.spellCastData = spellCastData;
            this.movementScripts = movementScripts.ToList();

            statsSysBehaviour = owner.GetComponent<StatsSystemBehaviour>();
            castCircleAnimator = castCircle.GetComponent<Animator>();
            
            statsSys = statsSysBehaviour.StatsSystem;
            
            prevEnableStates = new bool[movementScripts.Length];

            InitTimer();
        }

        private void InitTimer()
        {
            castingTimeWait = new WaitForSeconds(spellCastData.CastingTime);
            castCooldownTimer = new DownTimer(spellCastData.CastCooldownDelay);
            castCooldownTimer.OnTimerEnd += castCooldownTimer.DisableTimer;
        }

        protected virtual void Update()
        {
            castCooldownTimer.TryTick(Time.deltaTime);
        }

        public void CastSpell() => StartCoroutine(StartSpellCast());
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
