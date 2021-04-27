using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement
{
    public interface IMovementModifier
    {
        Vector3 Value { get; }
    }
}
