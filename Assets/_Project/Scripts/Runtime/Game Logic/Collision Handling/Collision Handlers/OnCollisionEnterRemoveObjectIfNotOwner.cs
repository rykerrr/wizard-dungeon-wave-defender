using UnityEngine;
using WizardGame.ObjectRemovalHandling;

namespace WizardGame.CollisionHandling
{
    public class OnCollisionEnterRemoveObjectIfNotOwner : MonoBehaviour, ICollisionHandler
    {
        private IRemovalProcessor removalProcessor;
        private GameObject owner;

        public void Init(GameObject owner)
        {
            this.owner = owner;

            removalProcessor = GetComponent<IRemovalProcessor>();
        }
        
        public void ProcessCollision(GameObject other, CollisionType type)
        {
            if (type != CollisionType.CollisionEnter && type != CollisionType.TriggerEnter) return;
            if (other == owner) return;
            
            removalProcessor.Remove();
        }
    }
}