using System;
using UnityEngine;
using UnityEngine.InputSystem;
using WizardGame.Movement;

#pragma warning disable 0649
public class TestReceiveForce : MonoBehaviour
{
    [SerializeField] private ForceReceiverMovementBehaviour forceReceiver;

    private void Update()
    {
        if (Keyboard.current.numpad0Key.wasPressedThisFrame)
        {
            forceReceiver.AddForce(-forceReceiver.transform.forward * 3f + Vector3.up * 2f);
        }
    }
}
