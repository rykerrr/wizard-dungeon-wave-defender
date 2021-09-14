using UnityEngine;
using WizardGame.Movement.Position;
using WizardGame.Utility.Patterns;

namespace WizardGame.Infrastructure.DataBuilders
{
    public class LocomotionMovementDataBuilder : AbstractDataBuilder<LocomotionMovement>
    {
        private float mvSpeed = default;
        private Vector2 input = default;
        
        public override LocomotionMovement Build()
        {
            var locomotion = new LocomotionMovement();
            
            locomotion.SetPreviousMovementInput(input);
            locomotion.MvSpeed = mvSpeed;

            return locomotion;
        }
        
        public LocomotionMovementDataBuilder() : this(Vector2.zero, 0f) { }

        public LocomotionMovementDataBuilder(float mvSpeed) : this(Vector2.zero, mvSpeed) { }

        public LocomotionMovementDataBuilder(Vector2 input) : this(input, 0f) { }
        
        public LocomotionMovementDataBuilder(Vector2 input, float mvSpeed)
        {
            this.input = input;
            this.mvSpeed = mvSpeed;
        }

        
        public LocomotionMovementDataBuilder WithInput(Vector2 input)
        {
            this.input = input;

            return this;
        }

        public LocomotionMovementDataBuilder WithSpeed(float mvSpeed)
        {
            this.mvSpeed = mvSpeed;

            return this;
        }
    }
}