using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Stats_System;

public class SpellBoltCastBehaviour : MonoBehaviour
{
    [SerializeField] private StatsSystemBehaviour statsSysBehav = default;
    [SerializeField] private List<MonoBehaviour> movementScripts = new List<MonoBehaviour>();
    
    [SerializeField] private GameObject spellBoltPrefab = default;
    [SerializeField] private GameObject vacuumParticleSys = default;
    [SerializeField] private GameObject castObj = default;
    [SerializeField] private float castCooldownTime = default;
    [SerializeField] private float timeToCast = default;
    [SerializeField] private float castSpd = 1f;

    private StatsSystem statsSystem = default;
    private WaitForSeconds castTime = default;
    private WaitForSeconds castCooldown = default;
    private StatBase intStat = default;

    private Vector3 hitPos;
    private Camera mainCam = default;
    private Animator castAnim = default;
    private bool canCast = true;

    private static readonly int Casting = Animator.StringToHash("Casting");
    private static readonly int CastSpeed = Animator.StringToHash("CastSpeed");
    
    private void Awake()
    {
        castAnim = castObj.GetComponent<Animator>();
        mainCam = Camera.main;
        
        statsSystem = statsSysBehav.StatsSystem;
        intStat = statsSystem.GetStat(StatTypeDB.GetType("Intelligence"));
        
        castTime = new WaitForSeconds(timeToCast);
        castCooldown = new WaitForSeconds(castCooldownTime);
    }

    private void Update()
    {
        if (!canCast) return;
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartCoroutine(CastSpell());
        }
    }

    private IEnumerator CastSpell()
    {
        castAnim.SetFloat(CastSpeed, castSpd);
        canCast = false;

        bool[] prevStates = new bool[movementScripts.Count];

        for (var i = 0; i < movementScripts.Count; i++)
        {
            prevStates[i] = movementScripts[i].enabled;
            movementScripts[i].enabled = false;
        }
        
        castObj.SetActive(true);
        vacuumParticleSys.SetActive(true);
        castAnim.SetBool(Casting, true);

        Vector3 spawnPos = transform.position + transform.forward * 2f + transform.up * 4f;
        
        castObj.transform.forward = transform.forward;
        castObj.transform.position = spawnPos;
        vacuumParticleSys.transform.position = spawnPos;

        GetHitPos();

        yield return castTime;

        for (var i = 0; i < movementScripts.Count; i++)
        {
            movementScripts[i].enabled = prevStates[i];
        }
            
        yield return castCooldown;

        canCast = true;
        yield return null;
    }

    private void GetHitPos()
    {
        Ray mouseRay = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());
        bool didHit = Physics.Raycast(mouseRay, out RaycastHit hitInfo, 500f);

        if (didHit)
        {
            hitPos = hitInfo.point;
        }
        else
        {
            hitPos = castObj.transform.forward * 50f;
        }
    }

    public void ExpelSpell()
    {
        if (!enabled || !gameObject.activeInHierarchy) return;
        
        var spellClone = Instantiate(spellBoltPrefab, castObj.transform.position, castObj.transform.rotation);

        var spellCloneScale = spellClone.transform.localScale;
        spellClone.transform.localScale =
            new Vector3(spellCloneScale.x, spellCloneScale.y, hitPos.z - spellClone.transform.position.z);
        // negative scale if not casting infront heheheheheh
        // rotate player towards mouse cast on cast
        
        spellClone.GetComponent<SpellBoltBehaviour>().InitSpell(hitPos, 1f, 1f, intStat.ActualValue,
            gameObject);

        castAnim.SetBool(Casting, false);
        vacuumParticleSys.SetActive(false);

        // spawn spell bolt at the end
        // pass hit target or max range
        // spawn explosion there
    }
}
