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

        private SpellCastBase castBase = default;

        public void Init(SpellCastBase castBase)
        {
            this.castBase = castBase;
        }
        
        public void InvokeCastEnd()
        {
            onCastEnd?.Invoke();
        }

        public void DisableSelf()
        {
            if (castBase.IsCasting) return;
            
            // Debug.Log("Self disabling spell circle..");
            gameObject.SetActive(false);
        }
    }
}
