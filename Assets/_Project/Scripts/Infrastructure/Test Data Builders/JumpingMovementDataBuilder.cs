using WizardGame.Movement.Position;
using WizardGame.Utility.Patterns;

namespace WizardGame.Infrastructure.DataBuilders
{
    public class JumpingMovementDataBuilder: AbstractDataBuilder<JumpingMovement>
    {
        private float jumpForce = default;
        private int input = default;
        
        public override JumpingMovement Build()
        {
            var jumpingMovement = new JumpingMovement();
            
            jumpingMovement.SetPreviousInput(input);
            jumpingMovement.JumpForce = jumpForce;

            return jumpingMovement;
        }

        public JumpingMovementDataBuilder() : this (0f, 0) { }
        
        public JumpingMovementDataBuilder(int input) : this(0f, input) { }

        public JumpingMovementDataBuilder(float jumpForce) : this(jumpForce, 0) { }
        
        public JumpingMovementDataBuilder(float jumpForce, int input)
        {
            this.jumpForce = jumpForce;
            this.input = input;
        }
        
        public JumpingMovementDataBuilder WithForce(float jumpForce)
        {
            this.jumpForce = jumpForce;

            return this;
        }

        public JumpingMovementDataBuilder WithInput(int input)
        {
            this.input = input;

            return this;
        }
    }
}