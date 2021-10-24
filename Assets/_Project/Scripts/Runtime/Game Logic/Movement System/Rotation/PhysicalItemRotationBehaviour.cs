using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Movement.Rotation
{
    public class PhysicalItemRotationBehaviour : MonoBehaviour
    {
        [SerializeField] private RotateObjectAroundAxis rotatePhysItemAroundAxis = default;
        [SerializeField] private RotateObjectTowardsTarget rotatePhysItemTowardsTarg = default;

        private readonly List<Transform> targetQueue = new List<Transform>();
        private Transform actualTarget = default;

        private void Update()
        {
            if (ReferenceEquals(actualTarget, null))
            {
                Debug.Log("attempting to rotate around axis");
                rotatePhysItemAroundAxis.Tick(Vector3.one, 1f);
            }
            else
            {
                rotatePhysItemTowardsTarg.Tick();
            }
        }

        public void OnCharacterEnter(Transform plr)
        {
            AddTarget(plr);

            if (actualTarget != null)
            {
                rotatePhysItemTowardsTarg.EnableRotate = true;
                rotatePhysItemAroundAxis.EnableRotate = false;
            }
        }

        public void OnCharacterExit(Transform plr)
        {
            RemoveTarget(plr);

            if (actualTarget == null)
            {
                rotatePhysItemTowardsTarg.EnableRotate = false;
                rotatePhysItemAroundAxis.EnableRotate = true;
            }
        }

        private void AddTarget(Transform plr)
        {
            if (targetQueue.Contains(plr)) return;

            targetQueue.Add(plr);

            if (actualTarget == null)
            {
                Debug.Log($"Current target doesn't exist, switching to: {targetQueue[0]}");

                actualTarget = targetQueue[0];

                rotatePhysItemTowardsTarg.Target = actualTarget;
            }
        }

        private void RemoveTarget(Transform plr)
        {
            if (targetQueue.Contains(plr)) targetQueue.Remove(plr);

            var o = actualTarget.gameObject;
            Debug.Log($"e.....{o}", o);
            
            if (actualTarget == plr)
            {
                if (targetQueue.Count > 0)
                {
                    Debug.Log($"Current target is being removed, switching to: {targetQueue[0]}", targetQueue[0]);

                    actualTarget = targetQueue[0];
                }
                else actualTarget = null;

                rotatePhysItemTowardsTarg.Target = actualTarget;
            }
        }
    }
}