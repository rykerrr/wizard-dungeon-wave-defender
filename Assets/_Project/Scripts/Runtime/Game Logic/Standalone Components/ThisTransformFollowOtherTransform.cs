using UnityEngine;

public class ThisTransformFollowOtherTransform : MonoBehaviour
{
    [SerializeField] private Transform objToFollow = default;

    private Vector3 offset = default;
    private Transform thisTransf = default;
    
    private void Awake()
    {
        thisTransf = transform;

        offset = thisTransf.position - objToFollow.position;
    }

    private void LateUpdate()
    {
        thisTransf.position = objToFollow.position + offset;
    }
}