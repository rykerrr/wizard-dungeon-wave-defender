using UnityEngine;

#pragma warning disable 0649
public class GravityBehavior : MonoBehaviour
{
    [SerializeField] private CharacterMotor motor;
    [SerializeField] private GravityMovementModifier gravity;
    
    public GravityMovementModifier Gravity => gravity;
    
    public void OnEnable() => motor.AddModifier(gravity);
    public void OnDisable() => motor.RemoveModifier(gravity);
    public void FixedUpdate() => gravity.Tick(Time.fixedDeltaTime);
}
