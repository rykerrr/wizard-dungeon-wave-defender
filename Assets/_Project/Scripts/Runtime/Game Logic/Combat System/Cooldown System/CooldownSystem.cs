using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WizardGame.CustomEventSystem;
using WizardGame.Utility.Patterns;

namespace WizardGame.Combat_System.Cooldown_System
{
    public class CooldownSystem : MonoBehaviour
    {
        private readonly List<Cooldown> cdData = new List<Cooldown>();

        public bool IsOnCooldown(Guid id)
        {
            foreach (var cd in cdData)
            {
                if (cd.Id != id) continue;

                var canTickGivenTimer = cd.CdTimer.IsTimerEnabled && cd.CdTimer.Time > 0;
                if (canTickGivenTimer) return true;
            }

            return false;
        }

        public void InitializeCooldowns(IHasCooldown[] cooldowns)
        {
            foreach (var cd in cooldowns)
            {
                AddCooldown(cd);
            }
        }
        
        public bool AddCooldown(IHasCooldown cd)
        {
            var cooldownExists = !ReferenceEquals(cdData.Find(x => x.Id == cd.Id), null);
            
            if (cooldownExists)
            {
                return false;
            }

            var data = new Cooldown(cd);
            cdData.Add(data);
            
            return true;
        }
        
        public bool RemoveCooldown(Guid id)
        {
            var data = cdData.Find(x => x.Id == id);
            
            return cdData.Remove(data);
        }

        // TODO: Turn into indexer property
        public Cooldown GetCooldown(Guid id)
        {
            return cdData.Find(x => x.Id == id);
        }

        public void Update()
        {
            ProcessCooldowns();
        }

        private void ProcessCooldowns()
        {
            var deltaTime = Time.deltaTime;

            for (var i = cdData.Count - 1; i >= 0; i--)
            {
                cdData[i].TryDecrementCooldown(deltaTime);
            }
        }

        [ContextMenu("Debug everything")]
        private void DebugAll()
        {
            foreach (var cd in cdData)
            {
                Debug.Log(cd.ToString());
            }
            Debug.Break();
        }
    }
}