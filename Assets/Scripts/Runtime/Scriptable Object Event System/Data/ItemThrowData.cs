using UnityEngine;
using WizardGame.ItemSystem.World_Interaction;

namespace WizardGame.CustomEventSystem
{
    public struct ItemThrowData
    {
        private PhysicalItem physItem;
        private Vector3 throwForce;

        public PhysicalItem PhysItem => physItem;
        public Vector3 ThrowForce => throwForce;

        public ItemThrowData(PhysicalItem physItem, Vector3 throwForce)
        {
            this.physItem = physItem;
            this.throwForce = throwForce;
        }
    }
}
