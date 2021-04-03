using UnityEngine;
using WizardGame.Movement;

namespace WizardGame.Utility.Infrastructure.DataBuilders
{
    public class MovementInputProcessorBuilder : TestDataBuilder<MovementInputProcessor>
    {
        private float speedMultiplier;
        private float smoothingMultiplier;
        
        public override MovementInputProcessor Build()
        {
            return new MovementInputProcessor(speedMultiplier, smoothingMultiplier);
        }

        public MovementInputProcessorBuilder(float speedMultiplier, float smoothingMultiplier)
        {
            this.speedMultiplier = speedMultiplier;
            this.smoothingMultiplier = smoothingMultiplier;
        }

        public MovementInputProcessorBuilder() : this(1, 1)
        {
            
        }

        public MovementInputProcessorBuilder WithSpeedMultiplier(float speedMultiplier)
        {
            this.speedMultiplier = speedMultiplier;

            return this;
        }

        public MovementInputProcessorBuilder WithSmoothingMultiplier(float smoothingMultiplier)
        {
            this.smoothingMultiplier = smoothingMultiplier;

            return this;
        }
    }
}