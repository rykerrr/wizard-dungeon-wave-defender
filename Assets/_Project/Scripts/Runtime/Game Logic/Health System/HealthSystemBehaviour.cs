using UnityEngine;
using WizardGame.Combat_System.Element_System;
using WizardGame.Combat_System.Element_System.Status_Effects;
using WizardGame.Stats_System;

namespace WizardGame.Health_System
{
    public class HealthSystemBehaviour : MonoBehaviour, IHealth
    {
        [SerializeField] private StatsSystemBehaviour statsSysBehaviour = default;
        [SerializeField] private StatusEffectHandler statusEffectHandler = default;
        
        private HealthSystem healthSystem = default;

        public HealthSystem HealthSystem => healthSystem ??= new HealthSystem(statsSysBehaviour.StatsSystem);
        public StatusEffectHandler StatusEffectHandler => statusEffectHandler;

        public int CurrentHealth => HealthSystem.CurrentHealth;
        public int MaxHealth => HealthSystem.MaxHealth;
        
        private void Awake()
        {
            HealthSystem.onDeathEvent += g => gameObject.SetActive(false);
        }

        private void Update()
        {
            HealthSystem.Tick();
        }

        public DamageResult TakeDamage(int dmg, Element damageElement, GameObject damageSource = null)
        {
            // Status effect creation + addition, possibly delegate to the status effect handler
            Debug.Log("Taking damage");
            
            var statEffData = damageElement.StatusEffectToApply;
            var statEff = StatusEffectFactory.CreateStatusEffect(statEffData,
                damageSource, damageElement, gameObject);

            StatusEffectAddResult statEffRes = StatusEffectAddResult.Failed;
            
            StatusEffectInteraction buff = default;
            
            try
            {
                statEffRes = StatusEffectHandler.AddStatusEffect(statEffData, statEff
                    , statEffData.Duration, out buff);
            }
            catch (System.Exception e)
            {
                Debug.Log($"Attempted to add status effect...Exception: {e}");
            }
            
            var res = healthSystem.TakeDamage(dmg, damageElement, damageSource);

            // Should not have to deal with this here...
            if (res.StatEffAddRes == StatusEffectAddResult.SpellBuff)
            {
                dmg = (int) System.Math.Round(dmg * buff.Effectiveness);
            }

            // health system doesnt care about the damage element technically...or well either the source lol
            HealthSystem.TakeDamage(dmg, damageElement, damageSource);

            res.StatEffAddRes = statEffRes;

            return res;
        }

        public void Heal(int hp, object source) => HealthSystem.Heal(hp, source);

        #region debug
        #if UNITY_EDITOR
        [ContextMenu("Dump health system data")]
        public void DumpHealthSystemData()
        {
            Debug.Log(HealthSystem.ToString());
        }
        #endif
        #endregion
    }
}