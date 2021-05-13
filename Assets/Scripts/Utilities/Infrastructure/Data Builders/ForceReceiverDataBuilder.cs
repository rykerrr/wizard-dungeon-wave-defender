using WizardGame.Movement;

namespace WizardGame.Utility.Infrastructure.DataBuilders
{
    public class ForceReceiverDataBuilder : TestDataBuilder<ForceReceiverMovement>
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