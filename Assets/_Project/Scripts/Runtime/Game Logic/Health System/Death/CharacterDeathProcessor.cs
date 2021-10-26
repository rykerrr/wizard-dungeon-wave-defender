using System;
using UnityEngine;

namespace WizardGame.Health_System.Death
{
    public class CharacterDeathProcessor : MonoBehaviour, IDeathProcessor
    {
        public event Action<GameObject> onDeathEvent;
        
        public bool HasDied { get; private set; }
        
        public void ProcessDeath(GameObject source = null)
        {
            HasDied = true;

            onDeathEvent?.Invoke(source);
        }
    }
}