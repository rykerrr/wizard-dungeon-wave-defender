using System;
using System.Linq;
using UnityEngine;

namespace WizardGame.ItemSystem.World_Interaction
{
    [Serializable]
    // Probably better to be renamed once i find a better name for it, it just supplies colliders of layer Interactable
    public class Interactor
    {
        [SerializeField] private LayerMask interactablesLayer;
        [SerializeField] private float findRange = default;
        [SerializeField] private float findWidth = default;

        private Vector3 HalfExtents => Vector3.one * findWidth / 2;

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

        public Collider[] FindInteractables(Vector3 center, Vector3 dir)
        {
            RaycastHit[] hits = Physics.BoxCastAll(center, HalfExtents, dir, Quaternion.identity, FindRange,
                interactablesLayer);

            return hits.Where(x => x.collider != null).Select(x => x.collider).ToArray();
        }
    }
}