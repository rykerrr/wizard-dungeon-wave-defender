using UnityEngine;
using WizardGame.Item_System.World_Interaction;

namespace WizardGame.CustomEventSystem
{
    public struct ItemThrowData
    {
        private InteractablePhysicalItem physItem;
        private Vector3 throwForce;

        public InteractablePhysicalItem PhysItem => physItem;
        public Vector3 ThrowForce => throwForce;

        public ItemThrowData(InteractablePhysicalItem physItem, Vector3 throwForce)
        {
            this.physItem = physItem;
            this.throwForce = throwForce;
        }
    }
}
