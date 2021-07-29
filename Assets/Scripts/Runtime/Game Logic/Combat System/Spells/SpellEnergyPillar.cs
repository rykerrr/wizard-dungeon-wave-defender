using System;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Combat_System;
using WizardGame.Health_System;
using WizardGame.Stats_System;
using WizardGame.Utility.Timers;

public class SpellEnergyPillar : MonoBehaviour, IDamagingSpell, IBuffingSpell
{
    [Header("References")]
    [SerializeField] private GameObject shockwaveEffect = default;

    [Header("Properties")]
    [SerializeField] private int amnOfShockwavesToCast = default;
    [SerializeField] private int maxShockwaveTargets = default;
    [SerializeField] private int avgShockwaveDmg = default;

    private DownTimer swTimer = default;
    private GameObject caster = default;
    
    private StatsSystemBehaviour casterStatsSysBehav = default;
    private StatType statKey = default;
    private StatModifier statModifier = default;
    
    private int actualSwDamage = default;
    private float swDelay = default;
    
    private Vector3 swCenter = default;
    private Vector3 swExtents = default;

    private Collider[] swHitColliders;
    public int ActualSwCount { get; private set; } = 0;

    private void Awake()
    {
        swHitColliders = new Collider[maxShockwaveTargets];
    }

    public void InitSpell(float shockwaveDmgMult, float swDelay, int amnOfShockwavesToCast, StatType statKey, StatModifier statModifier, GameObject caster)
    {
        this.caster = caster;
        this.statKey = statKey;
        this.statModifier = statModifier;
        this.amnOfShockwavesToCast = amnOfShockwavesToCast;
        this.swDelay = swDelay; 

        // this would work due to the collider having to be attached on the same object as this monobehaviour
        // the ontrigger calls would not work otherwise, but we want it to handle the buffing
        var thisCollider = GetComponent<BoxCollider>();
        swCenter = thisCollider.center;
        swExtents = thisCollider.size;
        
        casterStatsSysBehav = caster.GetComponent<StatsSystemBehaviour>();
        
        actualSwDamage = (int)Math.Round(avgShockwaveDmg * shockwaveDmgMult);
        
        InitTimer();
    }

    private void InitTimer()
    {
        swTimer = new DownTimer(swDelay);
        swTimer.OnTimerEnd += ProcessOnHit;
        swTimer.OnTimerEnd += swTimer.Reset;

        swTimer.OnTimerEnd += () =>
        {
            ActualSwCount++;

            if (ActualSwCount > amnOfShockwavesToCast)
            {
                swTimer.OnTimerEnd = null;
                Destroy(gameObject, 2f);
            }
        };
    }

    private void Update()
    {
        swTimer?.TryTick(Time.deltaTime);
    }

    public void ProcessOnHit()
    {
        var swClone = Instantiate(shockwaveEffect, transform.position, Quaternion.identity);
        swClone.transform.up = transform.up;

        var healthSysBehavs = GetHealthSystemsInBoxIgnoreCaster();
        healthSysBehavs.ForEach(x => x.HealthSystem.TakeDamage(actualSwDamage, caster));
    }

    private List<HealthSystemBehaviour> GetHealthSystemsInBoxIgnoreCaster()
    {
        Array.Clear(swHitColliders,0, swHitColliders.Length);
        
        var shockwaveHits = Physics.OverlapBoxNonAlloc(swCenter, swExtents, swHitColliders, Quaternion.identity,
            ~0, QueryTriggerInteraction.Ignore); 
        // hit every layer but ignore triggers
        
        var healthSystemBehaviours = new List<HealthSystemBehaviour>();

        for (var i = shockwaveHits - 1; i >= 0; i--)
        {
            if (swHitColliders[i].gameObject == caster) continue;

            HealthSystemBehaviour behav = default;

            if (!ReferenceEquals(behav = swHitColliders[i].GetComponent<HealthSystemBehaviour>(), null))
            {
                healthSystemBehaviours.Add(behav);
            }
        }

        return healthSystemBehaviours;
    }
    
    public void ApplyBuff(params StatsSystemBehaviour[] targets)
    {
        for (var i = targets.Length - 1; i >= 0; i--)
        {
            targets[i].StatsSystem.AddModifierTo(statKey, statModifier);
        }
    }

    public void RemoveBuff(params StatsSystemBehaviour[] targets)
    {
        for (var i = targets.Length - 1; i >= 0; i--)
        {
            Debug.Log(targets.Length + " | " + i);
            Debug.Log(targets[i]);
            Debug.Log(caster);
            
            targets[i].StatsSystem.RemoveModifierFrom(statKey, statModifier);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == caster)
        {
            ApplyBuff(casterStatsSysBehav);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == caster)
        {
            RemoveBuff(casterStatsSysBehav);
        }
    }

    public void OnDisable()
    {
        RemoveBuff(casterStatsSysBehav);
    }
}
