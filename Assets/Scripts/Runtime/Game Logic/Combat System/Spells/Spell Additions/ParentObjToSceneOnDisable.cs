using UnityEngine;

public class ParentObjToSceneOnDisable : MonoBehaviour
{
    [SerializeField] private Transform obj;
    [SerializeField] private float lifetime = 10;
    
    private void OnDisable()
    {
        obj.parent = null;
        
        Destroy(obj.gameObject, lifetime);
    }
}
