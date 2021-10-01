using WizardGame.Item_System.World_Interaction;
using UnityEngine;

namespace WizardGame.Movement.Rotation
{
    public class PhysicalItemRotationBehaviour : MonoBehaviour
    {
        [SerializeField] private RotateObjectAroundAxis rotatePhysItemAroundAxis = default;
        [SerializeField] private RotateObjectTowardsTarget rotatePhysItemTowardsTarg = default;

        private Transform actualTarget = default;

        private void Update()
        {
            if (ReferenceEquals(actualTarget, null))
            {
                rotatePhysItemAroundAxis.Tick(Vector3.one, 1f);
            }
            else
            {
                rotatePhysItemTowardsTarg.Tick(Vector3.one);
            }
        }

        public void OnPlayerEnter(Transform plr)
        {
            actualTarget = plr;
            
            rotatePhysItemTowardsTarg.Target = actualTarget;
        }

        public void OnPlayerExit(Transform plr)
        {
            if (plr != actualTarget) return; 
            
            actualTarget = null;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerInteractionBehavior>()) return;


        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.GetComponent<PlayerInteractionBehavior>()) return;
            

        }
    }
}