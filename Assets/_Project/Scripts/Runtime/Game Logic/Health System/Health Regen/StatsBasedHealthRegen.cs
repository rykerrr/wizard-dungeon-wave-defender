using System;
using UnityEngine;
using WizardGame.Stats_System;
using WizardGame.Utility.Timers;

namespace WizardGame.Health_System.HealthRegeneration
{
    public class StatsBasedHealthRegen : MonoBehaviour
    {
        [SerializeField] private StatsSystemBehaviour statsSysBehav = default;
        [SerializeField] private HealthSystemBehaviour healthSysBehav = default;

        private StatBase vigorStat;
        private StatBase resolveStat;

        private DownTimer healTimer;

        private int prevHealth = 0;

        private void Start()
        {
            InitStats();
            InitAutoHealTimer();
        }

        private void InitStats()
        {
            var statsSys = statsSysBehav.StatsSystem;

            vigorStat = statsSys.GetStat(StatTypeFactory.GetType("Vigor"));
            resolveStat = statsSys.GetStat(StatTypeFactory.GetType("Resolve"));
        }

        private void InitAutoHealTimer()
        {
            healTimer = new DownTimer(1f / vigorStat.ActualValue);

            healTimer.OnTimerEnd += () => healthSysBehav.Heal(resolveStat.ActualValue, this);
            healTimer.OnTimerEnd += () => healTimer.SetNewDefaultTime(1f / vigorStat.ActualValue);
            healTimer.OnTimerEnd += () => healTimer.Reset();
            
            healthSysBehav.HealthContainer.onHealthChange += (cur, max) =>
            {
                var wasDamaged = cur < prevHealth;
                if (wasDamaged) healTimer.Reset();

                prevHealth = cur;
            };
        }

        private void Update()
        {
            healTimer.TryTick(Time.deltaTime);
        }
    }
}