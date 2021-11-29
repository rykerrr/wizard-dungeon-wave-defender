using UnityEngine;

namespace WizardGame.CollisionHandling
{
    public class OnCollisionEnterRemoveObjectIfNotOwner : MonoBehaviour, ICollisionHandler
    {
        private GameObject owner;

        public void Init(GameObject owner)
        {
            this.owner = owner;
        }
        
        public void ProcessCollision(GameObject other, CollisionType type)
        {
            if (type != CollisionType.CollisionEnter && type != CollisionType.TriggerEnter) return;
            if (other == owner) return;
            
            Destroy(gameObject);
        }
    }
}