using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Stats_System;

namespace WizardGame.Combat_System.Mock_Up
{
    public class SimpleSpellCastBehaviour : MonoBehaviour
    {
        [SerializeField] private StatsSystemBehaviour statsSystemBehaviour = default;
        [SerializeField] private List<MonoBehaviour> movementScripts = new List<MonoBehaviour>();

        [SerializeField] private SpellBallBehaviour spellBallPrefab;
        [SerializeField] private GameObject castObj = default;
        [SerializeField] private float castCooldownTime = default;
        [SerializeField] private float timeToCast = default;

        private StatsSystem statsSystem = default;
        private Animator castAnim = default;
        private WaitForSeconds castTime = default;
        private WaitForSeconds castCooldown = default;
        private bool canCast = true;

        private StatBase intStat = default;
        
        private static readonly int Casting = Animator.StringToHash("Casting");

        private void Awake()
        {
            castAnim = castObj.GetComponent<Animator>();
            statsSystem = statsSystemBehaviour.StatsSystem;
            intStat = statsSystem.GetStat(StatTypeDB.GetType("Intelligence"));
            
            castCooldown = new WaitForSeconds(castCooldownTime);
            castTime = new WaitForSeconds(timeToCast);
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
            canCast = false;

            bool[] prevStates = new bool[movementScripts.Count];

            for (var i = 0; i < movementScripts.Count; i++)
            {
                prevStates[i] = movementScripts[i].enabled;
                movementScripts[i].enabled = false;
            }
            
            castObj.SetActive(true);
            castAnim.SetBool(Casting, true);
            
            castObj.transform.forward = transform.forward;
            castObj.transform.position = transform.position + transform.forward * 2f;

            yield return castTime;

            for (var i = 0; i < movementScripts.Count; i++)
            {
                movementScripts[i].enabled = prevStates[i];
            }
            
            yield return castCooldown;

            canCast = true;
            yield return null;
        }

        public void ExpelSpell()
        {
            if (!enabled || !gameObject.activeInHierarchy) return;
            
            var spellBolt = Instantiate(spellBallPrefab, castObj.transform.position, castObj.transform.rotation);
            spellBolt.InitSpell(1f, 1f, intStat.ActualValue, 1 + 1/intStat.ActualValue,
                gameObject);
            
            castAnim.SetBool(Casting, false);
            
            // castObj.transform.localPosition = Vector3.one * (int)Math.Round.Epsilon;
        }
    }
}
