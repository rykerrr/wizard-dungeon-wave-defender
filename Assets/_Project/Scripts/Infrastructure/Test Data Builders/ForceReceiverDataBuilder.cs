using WizardGame.Movement.Position;
using WizardGame.Utility.Patterns;

namespace WizardGame.Infrastructure.DataBuilders
{
    public class ForceReceiverDataBuilder : AbstractDataBuilder<ForceReceiverMovement>
    {
        private float drag = default;
        
        public override ForceReceiverMovement Build()
        {
            var forceReceiver = new ForceReceiverMovement();

            forceReceiver.Drag = drag;

            return forceReceiver;
        }

        public ForceReceiverDataBuilder() : this(0f) { }
        
        public ForceReceiverDataBuilder(float drag)
        {
            this.drag = drag;
        }
        
        public ForceReceiverDataBuilder WithDrag(float drag)
        {
            this.drag = drag;

            return this;
        }
    }
}