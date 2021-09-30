using System;
using System.Linq;
using UnityEngine;

namespace WizardGame.Item_System.World_Interaction
{
    [Serializable]
    public class GetClosestInteractable
    {
        [SerializeField] private LayerMask interactablesLayer;
        [SerializeField] private float findRange = default;
        [SerializeField] private float findWidth = default;
        
        private Vector3 HalfExtents => Vector3.one * findWidth / 2;
        private RaycastHit[] res = new RaycastHit[1];
        
        public float FindWidth
        {
            get => findWidth;
            set => findWidth = value;
        }

        public float FindRange
        {
            get => findRange;
            set => findRange = value;
        }

        public LayerMask InteractablesLayer
        {
            get => interactablesLayer;
            set => interactablesLayer = value;
        }

        public GameObject[] FindInteractables(Vector3 center, Vector3 dir)
        {
            // boxcast 5 targets, return nearest
            var hits = Physics.BoxCastNonAlloc(center, HalfExtents, dir, res, Quaternion.identity, FindRange, InteractablesLayer,
                QueryTriggerInteraction.Ignore);

            // Debug.Log($"Hit count: {hits}");
            
            return hits == 0 ? null : res.Select(x => x.rigidbody ? x.rigidbody.gameObject : x.collider.gameObject).ToArray();
        }
    }
}