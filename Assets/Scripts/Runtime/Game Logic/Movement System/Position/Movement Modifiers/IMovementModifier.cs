using UnityEngine;

#pragma warning disable 0649
namespace WizardGame.Movement.Position
{
    public interface IMovementModifier
    {
        float ExternalMult { get; set; }
        Vector3 Value { get; }
    }
}
