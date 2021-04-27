using UnityEngine;

#pragma warning disable 0649
public class NullMovementBehavior : MonoBehaviour
{
    [SerializeField] private CharacterMotor motor;
    [SerializeField] private JumpMovementModifier nullModifier;
    
    public JumpMovementModifier NullModifier => nullModifier;
    
    public void OnEnable() => motor.AddModifier(nullModifier);
    public void OnDisable() => motor.RemoveModifier(nullModifier);
    public void Update() => nullModifier.Tick(Time.deltaTime);
}
