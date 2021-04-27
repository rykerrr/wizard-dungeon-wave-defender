using UnityEngine;

#pragma warning disable 0649
public class NullMovementModifier : IMovementModifier
{
    public Vector3 Value { get; } = Vector3.zero;
    
    public void Tick(float deltaTime)
    {
        Debug.LogWarning("Using null object");    
    }
}
