using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
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
        [SerializeField] private float castingSpeed = default;

        protected static int BeginCastHash = Animator.StringToHash("BeginSpellCast");
        protected static int EndCastHash = Animator.StringToHash("EndSpellCast");
        
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
        public abstract BaseSpellCastData Data { get; set; }
        
        public bool CanCast => !isCasting && !cooldownSys.IsOnCooldown(Id);
        
        protected bool isCasting = false;
        public bool IsCasting => isCasting;

        private List<MonoBehaviour> movementScripts = new List<MonoBehaviour>();
        private CooldownData cdData = default;
        private EventSystem curEvSystem = default;
        private bool[] prevEnableStates;

        // change owner param type to StatsSystemBehaviour since we REQUIRE it?
        // just init castCircle or also instantiate it here? think init fits more
        public virtual void Init(GameObject owner, StatsSystem statsSys, CooldownSystem cooldownSys
            , Guid id, CastPlaceholder castCircle, BaseSpellCastData data, Element element
            , params MonoBehaviour[] movementScripts)
        {
            Owner = owner;

            this.id = id;
            this.movementScripts = movementScripts.ToList();
            this.cooldownSys = cooldownSys;
            this.statsSys = statsSys;
            this.castCircle = castCircle;
            this.element = element;
            
            castCircleAnimator = castCircle.GetComponent<Animator>();

            Data = data;
            
            curEvSystem = EventSystem.current;
            prevEnableStates = new bool[movementScripts.Length];
            
            InitTimer();
        }

        protected virtual void Awake()
        {
            // InitTimer();
        }

        private void InitTimer()
        {
            castingTimeWait = new WaitForSeconds(castingSpeed);
            
            cdData = cooldownSys.GetCooldown(Id);
        }
        
        public void CastSpell()
        {
            if (!CanCast  || curEvSystem.IsPointerOverGameObject())
            {
#if UNITY_EDITOR
                Debug.Log($"Can't cast due to: Timer Enabled: {cdData.CdTimer.IsTimerEnabled}" +
                          $" | Is casting: {isCasting} | Cooldown for cast left: {cdData.CdTimer.Time}");
#endif

                return;
            }
            
            StartCoroutine(StartSpellCast());
        }

        protected abstract IEnumerator StartSpellCast();
        public abstract void FinishSpellCast();

        protected void EnableCastCooldown()
        {
            cdData.CdTimer.Reset();
            cdData.CdTimer.EnableTimer();
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
        
        [ContextMenu("Log GUID")]
        public void LogId()
        {
            Debug.Log(id);
        }
    }
}
