using System;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable 0649
public class TestingOnInputActions : MonoBehaviour
{
    private PlayerInput plrInput;
    public void Awake()
    {
        plrInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (plrInput.currentActionMap.enabled) plrInput.currentActionMap.Disable();
            else plrInput.currentActionMap.Enable();
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            print($"{plrInput.currentActionMap["Attack"].phase}");
        }
    }

    private void OnJump(InputValue value)
    {
        Jump((float) value.Get());
    }

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        Movement(ctx.ReadValue<Vector2>());
    }

    public void OnAttack(InputValue value)
    {
        Attack(value.isPressed);
    }

    private void Jump(float jumpForce)
    {
        Debug.Log($"juuuuuuuuump {jumpForce}");
    }

    private void Movement(Vector2 movementInput)
    {
        Debug.Log($"moveeeeeeeeeee {movementInput}");
    }

    private void Attack(bool yes)
    {
        Debug.Log(yes);
        
        Debug.Log("attackui", this);
    }
}
