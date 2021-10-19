using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.CustomEventSystem;

namespace WizardGame.Combat_System
{
    public class CastPlaceholder : MonoBehaviour
    {
        public Action onCastEnd = delegate { };
        
        public void InvokeCastEnd()
        {
            onCastEnd?.Invoke();
        }

        public void DisableSelf() => gameObject.SetActive(false);
    }
}
