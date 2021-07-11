using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.CustomEventSystem;

namespace WizardGame.Combat_System
{
    public class CastPlaceholder : MonoBehaviour
    {
        [SerializeField] private VoidGameEvent onCastEnd;
        
        public void InvokeCastEnd()
        {
            onCastEnd.Raise();
        }

        public void DisableSelf() => gameObject.SetActive(false);
    }
}
