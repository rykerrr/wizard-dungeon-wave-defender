using System;
using UnityEngine;

#pragma warning disable 0649
public class SlidingMovementBehavior : MonoBehaviour
{
    [SerializeField] private CharacterMotor motor;
    [SerializeField] private SlidingDIY slidingMovement;

    public SlidingDIY SlidingMovement => slidingMovement;

    private void Awake()
    {
        slidingMovement.Init();
    }

    private void OnEnable() => motor.AddModifier(slidingMovement);
    private void OnDisable() => motor.RemoveModifier(slidingMovement);

    private void FixedUpdate() => SlidingMovement.Tick(Time.fixedDeltaTime);

    public bool ShouldSlide => SlidingMovement.ShouldSlide();
}
