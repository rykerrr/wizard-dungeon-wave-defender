using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Combat_System;
using WizardGame.Stats_System;

public class SpellCastEnergyPillar : SpellCastBase
{
    // the spell cast is similar to the explosion although it creates a pillar at the mouse position
    // instead of an explosion on the player
    // IF the mouse pos is over an object

    [SerializeField] private SpellEnergyPillar spellPrefab;

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
        set
        {
            value ??= new EnergyPillarData();

            if (value is EnergyPillarData newData)
            {
                data = newData;
            }
            else Debug.LogWarning("Passed data isn't null and can't be cast to EnergyBoltData");
        }
    }
    
    private void Awake()
    {
        mainCam = Camera.main;
    }

    public override void Init(GameObject owner, CastPlaceholder castCircle
        , BaseSpellCastData data, params MonoBehaviour[] movementScripts)
    {
        base.Init(owner, castCircle, data, movementScripts);

        ownerTransf = Owner.transform;
        castCircleTransf = castCircle.transform;
        intStat = statsSys.GetStat(StatTypeDB.GetType("Intelligence"));
    }
    
    protected override IEnumerator StartSpellCast()
    {
        pillarSpawnPos = GetMouseHitPos();

        isCasting = true;
        DeactivateMovementScripts();
        castCircle.gameObject.SetActive(true);

        castCircleTransf.position = ownerTransf.position;
        
        castCircleAnimator.SetBool(BeginCastHash, true);

        yield return castingTimeWait;

        castCircleAnimator.SetBool(EndCastHash, true);
    }

    public override void FinishSpellCast()
    {
        var spellClone = Instantiate(spellPrefab, pillarSpawnPos, Quaternion.identity);
        spellClone.transform.up = pillarSpawnNormal;

        var statKey = StatTypeDB.GetType("Vigor");
        var statModifierToApply = new StatModifier(ModifierType.Flat, 20f, Owner);
        
        spellClone.InitSpell(data.ShockwaveDamage, data.DelayBetweenWaves, data.ShockwaveAmount, statKey, statModifierToApply, Owner);
            
        castCircleAnimator.SetBool(BeginCastHash, false);
        castCircleAnimator.SetBool(EndCastHash, false);

        EnableCastCooldown();
        ReactivateMovementScripts();

        isCasting = false;
    }
    
    private Vector3 GetMouseHitPos()
    {
        var mouseRay = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        var didHit = Physics.Raycast(mouseRay, out RaycastHit hitInfo, 500f);

        pillarSpawnNormal = hitInfo.normal;
        
        return didHit ? hitInfo.point : Vector3.zero;
    }
}
