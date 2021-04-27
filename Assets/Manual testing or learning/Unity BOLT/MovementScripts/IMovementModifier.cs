using UnityEngine;

public interface IMovementModifier
{
    Vector3 Value { get; }
    void Tick(float deltaTime);
}
