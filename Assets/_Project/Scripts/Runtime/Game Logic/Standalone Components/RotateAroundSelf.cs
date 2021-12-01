using UnityEngine;

namespace WizardGame
{
    public class RotateAroundSelf : MonoBehaviour
    {
        [SerializeField] private Vector3 axis = Vector3.one;
        [SerializeField] private float angle = 3f;

        private Transform thisTransform;

        private void Awake()
        {
            thisTransform = transform;
        }

        private void Update()
        {
            thisTransform.Rotate(axis, angle);
        }
    }
}