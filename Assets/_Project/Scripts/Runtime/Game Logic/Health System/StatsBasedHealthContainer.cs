using System;
using System.Text;
using UnityEngine;
using WizardGame.Combat_System.Element_System;
using WizardGame.Health_System.Death;
using WizardGame.Stats_System;

namespace WizardGame.Health_System
{
    public class StatsBasedHealthContainer
    {
        // GameObject as falling rocks don't need to directly be living beings
        public event Action<int, int> onHealthChange = delegate { };

        private StatsSystem statsSys = default;
        private StatBase maxHealthStat = default;
        private IDeathProcessor deathProcessor;

        private int currentHealth = default;

        public int CurrentHealth => currentHealth;
        public int MaxHealth => maxHealthStat.ActualValue;


        private StringBuilder sb;

        public StatsBasedHealthContainer(StatsSystem statsSys, IDeathProcessor deathProcessor)
        {
            this.deathProcessor = deathProcessor;
            
            Init(statsSys);
        }

        private void Init(StatsSystem statsSys)
        {
            sb = new StringBuilder();

            this.statsSys = statsSys;

            maxHealthStat = this.statsSys.GetStat(StatTypeFactory.GetType("Max Health"));

            currentHealth = MaxHealth;
        }

        public void TakeDamage(int dmg, Element damageElement, GameObject damageSource = null)
        {
            currentHealth = Mathf.Clamp(currentHealth - dmg, 0, MaxHealth);

            if (!deathProcessor.HasDied && currentHealth == 0)
            {
                Death(damageSource);
            }

            onHealthChange?.Invoke(currentHealth, MaxHealth);
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
            deathProcessor.ProcessDeath(source);
        }

        public override string ToString()
        {
            // cur health / max health | curhp/maxhp percentage

            sb.Clear();
            sb.Append("Health/MaxHealth: ").Append(CurrentHealth).Append("/").Append(MaxHealth)
                .AppendLine();
            sb.Append("Health Percentage: ").Append(Math.Round((float) CurrentHealth / MaxHealth, 3) * 100f)
                .Append("%").AppendLine();

            return sb.ToString();
        }
    }
}