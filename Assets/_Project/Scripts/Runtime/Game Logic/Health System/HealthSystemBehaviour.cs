using UnityEngine;
using WizardGame.Combat_System.Element_System;
using WizardGame.Combat_System.Element_System.Status_Effects;
using WizardGame.Health_System.Death;
using WizardGame.Stats_System;

namespace WizardGame.Health_System
{
    public class HealthSystemBehaviour : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] private StatsSystemBehaviour statsSysBehaviour = default;
        [SerializeField] private StatusEffectHandler statusEffectHandler = default;
        
        private StatsBasedHealthContainer statsBasedHealthContainer = default;
        public StatsBasedHealthContainer StatsBasedHealthContainer => statsBasedHealthContainer ??= new StatsBasedHealthContainer(statsSysBehaviour.StatsSystem, DeathProcessor);

        public IDeathProcessor DeathProcessor { get; private set; }
        public StatusEffectHandler StatusEffectHandler => statusEffectHandler;

        private void Awake()
        {
            InitDeathProcessor();
        }

        private void InitDeathProcessor()
        {
            DeathProcessor = GetComponent<IDeathProcessor>();
            DeathProcessor.onDeathEvent += g => gameObject.SetActive(false);
        }

        public void TakeDamage(int dmg, Element damageElement, GameObject damageSource = null)
         => StatsBasedHealthContainer.TakeDamage(dmg, damageElement, damageSource);

        public void Heal(int hp, object source) => StatsBasedHealthContainer.Heal(hp, source);

        #region debug
        #if UNITY_EDITOR
        [ContextMenu("Dump health system data")]
        public void DumpHealthSystemData()
        {
            Debug.Log(StatsBasedHealthContainer.ToString());
        }
        #endif
        #endregion
    }
}