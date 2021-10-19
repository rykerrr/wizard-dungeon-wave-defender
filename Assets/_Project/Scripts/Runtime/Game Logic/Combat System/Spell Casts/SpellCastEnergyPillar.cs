using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Combat_System;
using WizardGame.Combat_System.Cooldown_System;
using WizardGame.Combat_System.Element_System;
using WizardGame.Stats_System;

public class SpellCastEnergyPillar : SpellCastBase
{
    // the spell cast is similar to the explosion although it creates a pillar at the mouse position
    // instead of an explosion on the player
    // IF the mouse pos is over an object

    private EnergyPillarData data;

    private Camera mainCam = default;
    private Transform ownerTransf = default;
    private Transform castCircleTransf = default;
    private StatBase intStat = default;

    private Vector3 pillarSpawnPos = Vector3.zero;
    private Vector3 pillarSpawnNormal = Vector3.zero;

    public override BaseSpellCastData Data
    {
        get => data;
        protected set
        {
            value ??= new EnergyPillarData();

            if (value is EnergyPillarData newData)
            {
                data = newData;
            }
            else Debug.LogWarning("Passed data isn't null and can't be cast to EnergyBoltData");
        }
    }

    public override void Init(GameObject owner, StatsSystem statsSys, CooldownSystem cooldownSys
        , Guid id, Transform castCirclePlacement, CastPlaceholder castCircle, BaseSpellCastData data, SpellBase spellPrefab
        , params MonoBehaviour[] movementScripts)
    {
        base.Init(owner, statsSys, cooldownSys, id, castCirclePlacement, castCircle, data
            , spellPrefab, movementScripts);

        ownerTransf = Owner.transform;
        castCircleTransf = castCircle.transform;
        intStat = statsSys.GetStat(StatTypeFactory.GetType("Intelligence"));
    }

    protected override void Awake()
    {
        mainCam = Camera.main;
    }

    protected override IEnumerator StartSpellCast()
    {
        isCasting = true;

        pillarSpawnPos = GetMouseHitPos();

        DeactivateMovementScripts();
        castCircle.gameObject.SetActive(true);

        castCircleTransf.position = castCirclePlacement.position;

        castCircleAnimator.SetBool(BeginCastHash, true);

        yield return castingTimeWait;

        castCircleAnimator.SetBool(EndCastHash, true);
    }

    public override void FinishSpellCast()
    {
        var statKey = StatTypeFactory.GetType("Vigor");
        var statModifierToApply = new StatModifier(ModifierType.Flat, 20f, Owner);

        var shockwaveDmg = data.BaseShockwaveDamage * Element.ElementSpellData.ExplosionStrengthMult;

        CreateSpellObject(shockwaveDmg, statKey, statModifierToApply);

        castCircleAnimator.SetBool(BeginCastHash, false);
        castCircleAnimator.SetBool(EndCastHash, false);

        EnableCastCooldown();
        ReactivateMovementScripts();

        isCasting = false;
    }

    private void CreateSpellObject(float shockwaveDmg, StatType statKey, StatModifier statModifierToApply)
    {
        var spellClone = (SpellEnergyPillar) Instantiate(spellPrefab, pillarSpawnPos, Quaternion.identity);
        spellClone.transform.up = pillarSpawnNormal;

        spellClone.InitSpell(shockwaveDmg, data.DelayBetweenWaves, data.ShockwaveAmount, statKey, statModifierToApply,
            Owner);
    }

    private Vector3 GetMouseHitPos()
    {
        var mouseRay = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        var didHit = Physics.Raycast(mouseRay, out RaycastHit hitInfo, 500f);

        pillarSpawnNormal = hitInfo.normal;

        return didHit ? hitInfo.point : Vector3.zero;
    }
}