using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WizardGame.Movement.Rotation.ForwardModifiers
{
    public class SetSelectedForwardModifier : MonoBehaviour
    {
        // Definitely better controlled by a state machine but this works for now
        
        [SerializeField] private SetObjectForward forwSetter = default;
        [SerializeField] private SetObjectForwardTowardsMouse setTowardsMouse = default;
        [SerializeField] private SetObjectForwardTowardsMovementVector setTowardsMvVector = default;

        private void Awake()
        {
            forwSetter.SetSelectedModifier(setTowardsMvVector);
        }

        public void Input_SetForwTowardsMouse(InputAction.CallbackContext ctx)
        {
            if (ctx.phase == InputActionPhase.Performed)
            {
                forwSetter.SetSelectedModifier(setTowardsMouse);
            }
            else
            {
                forwSetter.SetSelectedModifier(setTowardsMvVector);
            }
        }
    }
}