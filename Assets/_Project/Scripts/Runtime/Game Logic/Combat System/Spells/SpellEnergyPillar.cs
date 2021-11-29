using System;
using UnityEngine;
using WizardGame.CollisionHandling;
using WizardGame.Combat_System;
using WizardGame.Combat_System.Spell_Effects;
using WizardGame.Stats_System;
using WizardGame.Utility.Timers;

public class SpellEnergyPillar : SpellBase
{
    [Header("References")] [SerializeField]
    private PillarShockwave pillarShockwaveEffect = default;

    [Header("Properties, do not change in prefab variants")] [SerializeField]
    private LayerMask entityLayers;
    [SerializeField] private int amnOfShockwavesToCast = default;
    [SerializeField] private int maxShockwaveTargets = default;
    [SerializeField] private int avgShockwaveDmg = default;

    private DownTimer swTimer = default;
    private OnTriggerEnterApplyBuffToOwner buffApplier;
    private OnTriggerExitRemoveBuffFromOwner buffRemover;

    private Collider[] swHitColliders;

    private int actualSwDamage = default;
    private float swDelay = default;

    private Vector3 swCenter = default;
    private Vector3 swExtents = default;

    public int ActualSwCount { get; private set; } = 0;

    public void InitSpell(float shockwaveDmgMult, float swDelay, int amnOfShockwavesToCast, StatType statKey,
        StatModifier statModifier, GameObject caster)
    {
        this.caster = caster;
        this.amnOfShockwavesToCast = amnOfShockwavesToCast;
        this.swDelay = swDelay;

        // this would work due to the collider having to be attached on the same object as this monobehaviour
        // the ontrigger calls would not work otherwise, but we want it to handle the buffing
        var thisCollider = GetComponent<BoxCollider>();
        swCenter = thisCollider.center;
        swExtents = thisCollider.size;

        swHitColliders = new Collider[maxShockwaveTargets];

        actualSwDamage = (int) Math.Round(avgShockwaveDmg * shockwaveDmgMult);

        buffApplier = GetComponent<OnTriggerEnterApplyBuffToOwner>();
        buffRemover = GetComponent<OnTriggerExitRemoveBuffFromOwner>();
        buffApplier.Init(statKey, statModifier, caster);
        buffRemover.Init(statKey, statModifier, caster);

        InitTimer();
    }

    private void InitTimer()
    {
        swTimer = new DownTimer(swDelay);

        swTimer.OnTimerEnd += CreateShockwave;

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

    public void CreateShockwave()
    {
        var swClone = Instantiate(pillarShockwaveEffect, transform.position, Quaternion.identity);

        swClone.transform.up = transform.up;

        swClone.Init(actualSwDamage, swExtents, SpellElement, Caster, entityLayers, ref swHitColliders);
    }
}