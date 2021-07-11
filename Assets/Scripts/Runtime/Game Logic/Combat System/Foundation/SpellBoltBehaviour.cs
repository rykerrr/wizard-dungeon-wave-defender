using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Health_System;

public class SpellBoltBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject explosionEffect = default;

    [SerializeField] private float avgExplosionRadius = default;
    [SerializeField] private int avgImpactDmg = default;
    [SerializeField] private int avgExplosionDmg = default;

    private GameObject objHit = default;
    private GameObject caster = default;
    private Vector3 hitPos = default;
    private int actualImpactDmg = default;
    private int actualExplosionDmg = default;
    private float actualRadius = default;
    
    public void InitSpell(Vector3 hitPos, float explosionRadMult
        , float onHitDmgMult, float explosionDmgMult, GameObject caster, GameObject objHit = null)
    {
        actualImpactDmg = (int)Math.Round(avgImpactDmg * onHitDmgMult);
        actualExplosionDmg = (int)Math.Round(avgExplosionDmg * explosionDmgMult);
        actualRadius = avgExplosionRadius * explosionRadMult;

        this.objHit = objHit;
        this.caster = caster;
        this.hitPos = hitPos;

        OnHit();
    }

    private void OnHit()
    {
        var explClone = Instantiate(explosionEffect, hitPos, Quaternion.identity);
        explClone.transform.localScale = Vector3.one * avgExplosionRadius;
        
        Collider[] explosionHits = Physics.OverlapSphere(hitPos, actualRadius);
        List<HealthSystemBehaviour> healthSystemBehaviours = new List<HealthSystemBehaviour>();

        foreach (var target in explosionHits)
        {
            HealthSystemBehaviour behav = default;

            if (!ReferenceEquals(behav = target.GetComponent<HealthSystemBehaviour>(), null))
            {
                healthSystemBehaviours.Add(behav);
            }
        }
        
        healthSystemBehaviours.ForEach(x => x.HealthSystem.TakeDamage(actualExplosionDmg, caster));

        HealthSystemBehaviour hitTarget = null;

        if (!ReferenceEquals(objHit, null) && !ReferenceEquals(hitTarget = objHit.GetComponent<HealthSystemBehaviour>(), null))
        {
            hitTarget.HealthSystem.TakeDamage(actualImpactDmg, caster);
        }
        
        Destroy(explClone, 1.5f);
        Destroy(gameObject, 1f);
    }
}
