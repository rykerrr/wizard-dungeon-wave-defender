using UnityEngine;

namespace WizardGame.Movement
{
    public interface IMovementModifier
    {
        Vector3 Value { get; }
    }
}