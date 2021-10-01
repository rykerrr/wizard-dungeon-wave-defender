using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Movement.Position;

namespace WizardGame.Item_System.World_Interaction
{
    public class SetMonoBehavioursActiveAfterWait : MonoBehaviour
    {
        [SerializeField] private float secondsToWait = default;

        private WaitForSeconds waitTime;

        public void ForceMonoBehaviourSet(bool setEnabled, params Behaviour[] monoBehavs)
        {
            foreach (var mb in monoBehavs)
            {
                mb.enabled = setEnabled;
                
                Debug.Log($"{mb} set enabled: {setEnabled}!");
            }            
        }

        public void WaitEnableMonoBehaviour(bool enable, params Behaviour[] monoBehavs)
        {
            StartCoroutine(EnablePhysicsCoroutine(enable, monoBehavs));
        }

        private IEnumerator EnablePhysicsCoroutine(bool enable, params Behaviour[] monoBehavs)
        {
            waitTime ??= new WaitForSeconds(secondsToWait);

            yield return waitTime;

            ForceMonoBehaviourSet(enable, monoBehavs);
        }
    }
}