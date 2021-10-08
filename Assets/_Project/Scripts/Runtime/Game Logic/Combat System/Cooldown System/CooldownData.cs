using System;
using UnityEngine;

namespace WizardGame.Combat_System.Cooldown_System
{
    [Serializable]
    public class CooldownData : IHasCooldown
    {
        [Header("Cooldown data")] 
        [SerializeField] private float cooldownDuration;
        
        // TODO: Create a serializable GUID
        [SerializeField] private Guid id = Guid.NewGuid();
        
        public Guid Id => id;
        public float CooldownDuration => cooldownDuration;

        public CooldownData() { }

        public CooldownData(float cooldownDuration, bool initGuid = false) =>
            Init(cooldownDuration, initGuid);
        
        public void Init(float cooldownDuration, bool initGuid = false)
        {
            this.cooldownDuration = cooldownDuration;
            
            if(initGuid) id = Guid.NewGuid();
        }
    }
}