using System;
using System.Text;
using UnityEngine;
using WizardGame.Combat_System.Element_System;
using WizardGame.Stats_System;
using WizardGame.Utility.Timers;

namespace WizardGame.Health_System
{
    [Serializable]
    public class HealthSystem : IHealth
    {
        [SerializeField] private int currentHealth = default;

        // GameObject as falling rocks don't need to directly be living beings
        public event Action<GameObject> onDeathEvent = delegate { };
        public event Action<int, int> onHealthChange = delegate { };

        private DownTimer healTimer = default;
        private StatsSystem statsSys = default;
        private StatBase maxHealthStat = default;
        private StatBase vigorStat = default;
        private StatBase resolveStat = default;

        public int CurrentHealth => currentHealth;
        public int MaxHealth => maxHealthStat.ActualValue;

        private bool hasDied = default;

        private StringBuilder sb;

        public HealthSystem(StatsSystem statsSys)
        {
            Init(statsSys);
        }

        public void Init(StatsSystem statsSys)
        {
            sb = new StringBuilder();

            this.statsSys = statsSys;

            maxHealthStat = this.statsSys.GetStat(StatTypeFactory.GetType("Max Health"));
            vigorStat = this.statsSys.GetStat(StatTypeFactory.GetType("Vigor"));
            resolveStat = this.statsSys.GetStat(StatTypeFactory.GetType("Resolve"));

            currentHealth = MaxHealth;

            InitAutoHealTimer();
        }

        private void InitAutoHealTimer()
        {
            healTimer = new DownTimer(1f / vigorStat.ActualValue);

            healTimer.OnTimerEnd += () => Heal(resolveStat.ActualValue, this);
            healTimer.OnTimerEnd += () => healTimer.SetNewDefaultTime(1f / vigorStat.ActualValue);
            healTimer.OnTimerEnd += () => healTimer.Reset();
        }

        public void Tick()
        {
            var ticked = healTimer.TryTick(Time.deltaTime);
        }

        public DamageResult TakeDamage(int dmg, Element damageElement, GameObject damageSource = null)
        {
            currentHealth = Mathf.Clamp(currentHealth - dmg, 0, MaxHealth);
            healTimer.Reset();

            if (!hasDied && currentHealth == 0)
            {
                // Exp gain functions by last hit due to this
                Death(damageSource);
            }

            onHealthChange?.Invoke(currentHealth, MaxHealth);

            return new DamageResult(true);
        }

        public void Heal(int hp, object source)
        {
            if (currentHealth == 0)
            {
                return;
            }

            currentHealth = Mathf.Clamp(currentHealth + hp, 0, MaxHealth);

            onHealthChange?.Invoke(currentHealth, MaxHealth);
        }

        private void Death(GameObject source = null)
        {
            hasDied = true;

            onDeathEvent?.Invoke(source);
        }

        public override string ToString()
        {
            // cur health / max health | curhp/maxhp percentage | next heal time

            sb.Clear();
            sb.Append("Health/MaxHealth: ").Append(CurrentHealth).Append("/").Append(MaxHealth)
                .AppendLine();
            sb.Append("Health Percentage: ").Append(Math.Round((float) CurrentHealth / MaxHealth, 3) * 100f)
                .Append("%").AppendLine();
            sb.Append("Next heal in: ").Append(healTimer.Time).AppendLine();

            return sb.ToString();
        }
    }
}