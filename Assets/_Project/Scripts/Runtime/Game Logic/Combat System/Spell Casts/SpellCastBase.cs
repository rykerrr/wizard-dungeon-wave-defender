using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WizardGame.Combat_System.Cooldown_System;
using WizardGame.Combat_System.Element_System;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System
{
    public abstract class SpellCastBase : MonoBehaviour, IHasCooldown
    {
        // TODO: Figure out what to do with the IHasCooldown here, since the CooldownDuration is redundant
        
        [Header("Cooldown property")]
        [SerializeField] private Guid id = default;
        [SerializeField] private float castCooldown = default;
        
        [Header("Spell cast data")]
        [SerializeField] private float timeToCast = default;
        [SerializeField] protected SpellBase spellPrefab;

        protected static int BeginCastHash = Animator.StringToHash("BeginSpellCast");
        protected static int EndCastHash = Animator.StringToHash("EndSpellCast");

        protected Transform castCirclePlacement;
        protected CastPlaceholder castCircle = default;
        
        protected StatsSystem statsSys = default;
        protected CooldownSystem cooldownSys = default;
        protected Animator castCircleAnimator = default;
        protected WaitForSeconds castingTimeWait = default;

        protected Element element = default;
        
        public Guid Id => id;
        public float CooldownDuration => castCooldown;
        public Element Element => element;
        
        public GameObject Owner { get; private set; } = default;
        public abstract BaseSpellCastData Data { get; protected set; }

        public bool CanCast => !isCasting && !cooldownSys.IsOnCooldown(Id); //&& !castCircle.gameObject.activeSelf;
        
        protected bool isCasting = false;
        public bool IsCasting => isCasting;

        private List<MonoBehaviour> movementScripts = new List<MonoBehaviour>();
        private Cooldown cd = default;
        private bool[] prevEnableStates;

        // change owner param type to StatsSystemBehaviour since we REQUIRE it?
        // just init castCircle or also instantiate it here? think init fits more
        public virtual void Init(GameObject owner, StatsSystem statsSys, CooldownSystem cooldownSys
            , Guid id, Transform castCirclePlacement, CastPlaceholder castCircle, BaseSpellCastData data, SpellBase spellPrefab
            , params MonoBehaviour[] movementScripts)
        {
            // Debug.Log($"{owner} | NEXT: | {statsSys} | NEXT: | {cooldownSys} | NEXT: | {id} | NEXT: |" +
            //           $" {castCircle} | NEXT: | {data} | NEXT: | {spellPrefab} | NEXT: | {movementScripts.Length}");
            
            Owner = owner;
            element = spellPrefab.SpellElement;

            this.id = id;
            this.movementScripts = movementScripts.ToList();
            this.cooldownSys = cooldownSys;
            this.statsSys = statsSys;
            this.castCirclePlacement = castCirclePlacement;
            this.castCircle = castCircle;
            this.spellPrefab = spellPrefab;

            this.castCircle.onCastEnd += FinishSpellCast;
            castCircleAnimator = castCircle.GetComponent<Animator>();

            Data = data;
            
            prevEnableStates = new bool[movementScripts.Length];
            
            InitTimer();
        }

        protected virtual void Awake()
        {
            // InitTimer();
        }

        private void InitTimer()
        {
            castingTimeWait = new WaitForSeconds(timeToCast);
            
            cd = cooldownSys.GetCooldown(Id);
        }
        
        public void CastSpell()
        {
            if (!CanCast)
            {
#if UNITY_EDITOR
                Debug.Log($"Can't cast due to: Timer Enabled: {cd.CdTimer.IsTimerEnabled}" +
                          $" | Is casting: {isCasting} | Cooldown for cast left: {cd.CdTimer.Time}", Owner);
#endif

                return;
            }
            
            StartCoroutine(StartSpellCast());
        }

        protected abstract IEnumerator StartSpellCast();
        public abstract void FinishSpellCast();

        protected void EnableCastCooldown()
        {
            cd.CdTimer.Reset();
            cd.CdTimer.EnableTimer();
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

        protected virtual void OnDestroy()
        {
            cooldownSys.RemoveCooldown(Id);
        }
        
        #region Debug
        #if UNITY_EDITOR
        [ContextMenu("Log GUID")]
        public void LogId()
        {
            Debug.Log(id);
        }
        #endif
        #endregion
    }
}
