using System;
using UnityEngine;
using WizardGame.Combat_System;
using WizardGame.Combat_System.Spell_Effects;
using WizardGame.Stats_System;
using WizardGame.Utility.Timers;

public class SpellEnergyPillar : SpellBase, IDamagingSpell, IBuffingSpell
{
    [Header("References")]
    [SerializeField] private PillarShockwave pillarShockwaveEffect = default;

    [Header("Properties, do not change in prefab variants")]
    [SerializeField] private int amnOfShockwavesToCast = default;
    [SerializeField] private int maxShockwaveTargets = default;
    [SerializeField] private int avgShockwaveDmg = default;

    private DownTimer swTimer = default;

    private Collider[] swHitColliders;
    
    private StatsSystemBehaviour casterStatsSysBehav = default;
    private StatType statKey = default;
    private StatModifier statModifier = default;
    
    private int actualSwDamage = default;
    private float swDelay = default;
    
    private Vector3 swCenter = default;
    private Vector3 swExtents = default;

    public int ActualSwCount { get; private set; } = 0;

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
        swHitColliders = new Collider[maxShockwaveTargets];
        
        actualSwDamage = (int)Math.Round(avgShockwaveDmg * shockwaveDmgMult);
        
        InitTimer();
    }

    private void InitTimer()
    {
        swTimer = new DownTimer(swDelay);
        
        swTimer.OnTimerEnd += CreateOnHitEffect;

        swTimer.OnTimerEnd += () =>
        {
            ActualSwCount++;

            if (ActualSwCount > amnOfShockwavesToCast)
            {
                swTimer.OnTimerEnd = null;
                Destroy(gameObject, 2f);
            }
        };
        
        swTimer.OnTimerEnd += swTimer.Reset;
    }

    private void Update()
    {
        swTimer?.TryTick(Time.deltaTime);
    }

    public void CreateOnHitEffect()
    {
        var swClone = Instantiate(pillarShockwaveEffect, transform.position, Quaternion.identity);
        
        swClone.transform.up = transform.up;
        
        swClone.Init(actualSwDamage, swExtents, SpellElement.ElementColor, Caster, ref swHitColliders);
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
