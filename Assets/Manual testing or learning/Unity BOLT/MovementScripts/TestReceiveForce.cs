using System;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Movement.Position;

#pragma warning disable 0649
public class TestReceiveForce : MonoBehaviour
{
    [SerializeField] private ForceReceiverMovementBehaviour forceReceiver = default;

    private void Update()
    {
        if (Keyboard.current.numpad0Key.wasPressedThisFrame)
        {
            AddForce();
        }
    }
    
    [ContextMenu("Test add force")]
    private void AddForce()
    {
        forceReceiver.AddForce(-forceReceiver.transform.forward * 3f + Vector3.up * 2f);
    }
}
