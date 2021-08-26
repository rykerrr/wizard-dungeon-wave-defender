using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Combat_System.Element_System;
using WizardGame.Combat_System.Element_System.Status_Effects;

public class ApplyElementStatusEffectOnCollisionStay : MonoBehaviour
{
    [SerializeField] private Element elementToApply;

    private List<GameObject> ignoreObjs = new List<GameObject>();
    
    private void OnCollisionStay(Collision other)
    {
        if (ignoreObjs.Contains(other.gameObject)) return;
        
        if (other.rigidbody)
        {
            StatusEffectHandler statEffHandler;
            var statEffHandlerExists = statEffHandler = other.rigidbody.GetComponent<StatusEffectHandler>();

            if (!statEffHandlerExists)
            {
                ignoreObjs.Add(other.rigidbody.gameObject);
            }

            var statEffData = elementToApply.StatusEffectToApply;

            statEffHandler.AddStatusEffect(statEffData, StatusEffectFactory.CreateStatusEffect(statEffData),
                statEffData.Duration, out var buff);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (ignoreObjs.Contains(other.gameObject)) ignoreObjs.Remove(other.gameObject);
    }
}
