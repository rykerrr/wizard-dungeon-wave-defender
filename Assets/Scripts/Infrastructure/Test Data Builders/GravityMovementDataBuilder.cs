using WizardGame.Utility.Patterns;
using WizardGame.Movement.Position;

namespace WizardGame.Infrastructure.DataBuilders
{
    public class GravityMovementDataBuilder : AbstractDataBuilder<GravityMovement>
    {
        private float gravMagnitude = default;
        private float groundedPullMagnitude = default;
        
        public override GravityMovement Build()
        {
            var gravMovement = new GravityMovement();

            gravMovement.GravMagnitude = gravMagnitude;
            gravMovement.GroundedPullMagnitude = groundedPullMagnitude;

            return gravMovement;
        }

        public GravityMovementDataBuilder() : this(0f, 0f) { }
        
        public GravityMovementDataBuilder(float gravMagnitude, float groundedPullMagnitude)
        {
            this.gravMagnitude = gravMagnitude;
            this.groundedPullMagnitude = groundedPullMagnitude;
        }
        
        public GravityMovementDataBuilder WithGrav(float gravMagnitude)
        {
            this.gravMagnitude = gravMagnitude;

            return this;
        }

        public GravityMovementDataBuilder WithGroundPullMag(float groundedPullMagnitude)
        {
            this.groundedPullMagnitude = groundedPullMagnitude;

            return this;
        }
    }
}