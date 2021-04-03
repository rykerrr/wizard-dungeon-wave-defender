using UnityEngine;
using WizardGame.Movement;

namespace WizardGame.Behaviors
{
    public class MovementInputProcessorBehavior : MonoBehaviour
    {
        [SerializeField] private MovementInputProcessor movementInputProcessor;

        public MovementInputProcessor MovementInputProcessor => movementInputProcessor;
    }
}