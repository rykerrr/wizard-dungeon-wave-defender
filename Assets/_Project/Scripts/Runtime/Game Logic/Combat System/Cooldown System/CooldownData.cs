using System;
using System.Text;
using UnityEngine;
using WizardGame.Utility.Timers;

namespace WizardGame.Combat_System.Cooldown_System
{
    public class CooldownData
    {
        private Action<float, float> onDecrement = delegate { };
        
        private DownTimer cdTimer = default;

        private StringBuilder sb = new StringBuilder();
        
        public CooldownData(IHasCooldown cd)
        {
            cdTimer = new DownTimer(cd.CooldownDuration);
            cdTimer.OnTimerEnd += cdTimer.DisableTimer;

            Id = cd.Id;
        }

        public DownTimer CdTimer => cdTimer;
        public Guid Id { get; }

        public bool TryDecrementCooldown(float deltaTime)
        {
            var didTick = cdTimer.TryTick(deltaTime);
            
            if(didTick)
                onDecrement.Invoke(CdTimer.Time, CdTimer.DefaultTime);

            return didTick;
        }

        public void AddListenAction(Action<float, float> act)
        {
            onDecrement += act;
        }

        public void RemoveListenAction(Action<float, float> act)
        {
            onDecrement -= act;
        }

        public override string ToString()
        {
            sb.Clear();
            
            var delegs = (MulticastDelegate) onDecrement;

            sb.Append(Id).AppendLine();
            
            foreach (var del in delegs.GetInvocationList())
            {
                sb.Append(del.Target.GetType()).Append(" <--- Target, Delegate ---> ").Append(del).AppendLine();
            }
            
            sb.AppendLine();

            return sb.ToString();
        }
    }
}