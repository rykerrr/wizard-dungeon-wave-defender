using UnityEngine;
using WizardGame.Combat_System.Element_System.Status_Effects;
using WizardGame.Stats_System;

namespace WizardGame.Health_System
{
    public class HealthSystemBehaviour : MonoBehaviour
    {
        [SerializeField] private StatsSystemBehaviour statsSysBehaviour = default;
        [SerializeField] private StatusEffectHandler statusEffectHandler = default;
        
        private HealthSystem healthSystem = default;

        public HealthSystem HealthSystem => healthSystem ??= new HealthSystem(statsSysBehaviour.StatsSystem);
        public StatusEffectHandler StatusEffectHandler => statusEffectHandler;
        
        private void Awake()
        {
            HealthSystem.onDeathEvent += g => gameObject.SetActive(false);
        }

        private void Update()
        {
            HealthSystem.Tick();
        }
        
        #region debug
        [ContextMenu("Dump health system data")]
        public void DumpHealthSystemData()
        {
            Debug.Log(HealthSystem.ToString());
        }
        #endregion
    }
}