using System;
using System.Text;
using UnityEngine;
using WizardGame.Stats_System;
using WizardGame.Utility.Timers;

namespace WizardGame.Health_System
{
   [Serializable]
    public class HealthSystem
    {
        [SerializeField] private int curHealth = default;
        
        // GameObject as falling rocks don't need to directly be living beings
        public event Action<GameObject> onDeathEvent = delegate { };
        
        private DownTimer healTimer = default;
        private StatsSystem statsSys = default;
        private StatBase maxHealthStat = default;
        private StatBase vigorStat = default;
        private StatBase resolveStat = default;
        
        public int CurHealth => curHealth;

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
            
            maxHealthStat = this.statsSys.GetStat(StatTypeDB.GetType("Max Health"));
            vigorStat = this.statsSys.GetStat(StatTypeDB.GetType("Vigor"));
            resolveStat = this.statsSys.GetStat(StatTypeDB.GetType("Resolve"));
            
            curHealth = maxHealthStat.ActualValue;
            
            InitAutoHealTimer();
        }

        private void InitAutoHealTimer()
        {
            healTimer = new DownTimer(1f / vigorStat.ActualValue);
            Debug.Log(vigorStat.ActualValue);
            Debug.Log("" + (1f / vigorStat.ActualValue));
            Debug.Log("" + ((float)(1f / vigorStat.ActualValue)));
            Debug.Log(healTimer.Time + " | " + healTimer.DefaultTime);

            healTimer.OnTimerEnd += () =>
            {
                Debug.Log("Auto Heal Occurring!");
                Heal(resolveStat.ActualValue, this);
                
                healTimer.SetNewDefaultTime(1f / vigorStat.ActualValue);
                healTimer.Reset();
            };
            
            // healTimer.OnTimerEnd += () => Debug.Log("Auto Heal Occurring!");
            // healTimer.OnTimerEnd += () => healTimer.SetNewDefaultTime(1f / vigorStat.ActualValue);
            // healTimer.OnTimerEnd += () => Heal(resolveStat.ActualValue, this);
            // healTimer.OnTimerEnd += () => healTimer.Reset();
        }

        public void Tick()
        {
            var ticked = healTimer.TryTick(Time.deltaTime);
        }

        public void TakeDamage(int dmg, GameObject damageSource = null)
        {
            curHealth = Mathf.Clamp(curHealth - dmg, 0, maxHealthStat.ActualValue);
            healTimer.Reset();
            
            if (!hasDied && curHealth == 0)
            {
                // Exp gain functions by last hit due to this
                Death(damageSource);
            }
        }

        public void Heal(int hp, object source)
        {
            if (curHealth == 0)
            {
                return;
            }
            
            curHealth = Mathf.Clamp(curHealth + hp, 0, maxHealthStat.ActualValue);
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
            sb.Append("Health/MaxHealth: ").Append(CurHealth).Append("/").Append(maxHealthStat.ActualValue).AppendLine();
            sb.Append("Health Percentage: ").Append(Math.Round((float) CurHealth / maxHealthStat.ActualValue, 3) * 100f)
                .Append("%").AppendLine();
            sb.Append("Next heal in: ").Append(healTimer.Time).AppendLine();

            return sb.ToString();
        }
    }
}