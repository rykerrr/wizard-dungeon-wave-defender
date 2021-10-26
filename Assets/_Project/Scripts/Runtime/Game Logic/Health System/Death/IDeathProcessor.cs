using System;
using UnityEngine;

namespace WizardGame.Health_System.Death
{
    public interface IDeathProcessor
    {
        event Action<GameObject> onDeathEvent;
        
        bool HasDied { get; }

        void ProcessDeath(GameObject source);
    }
}