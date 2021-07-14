using UnityEngine;
using WizardGame.Stats_System;

namespace WizardGame.Health_System
{
    public class HealthSystemBehaviour : MonoBehaviour
    {
        [SerializeField] private StatsSystemBehaviour statsSysBehaviour = default;
        
        private HealthSystem healthSystem = default;

        public HealthSystem HealthSystem => healthSystem ??= new HealthSystem(statsSysBehaviour.StatsSystem);

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