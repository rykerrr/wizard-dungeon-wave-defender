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

        public void TakeDamage(int dmg, Element damageElement, GameObject damageSource = null)
         => HealthSystem.TakeDamage(dmg, damageElement, damageSource);

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