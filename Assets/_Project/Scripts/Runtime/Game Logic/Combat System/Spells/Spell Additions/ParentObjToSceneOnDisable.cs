using UnityEngine;

public class ParentObjToSceneOnDisable : MonoBehaviour
{
    [SerializeField] private Transform obj;
    [SerializeField] private float lifetime = 10;

    private bool appQuitting = false;
    
    private void OnDisable()
    {
        if (appQuitting) return;
        
        obj.parent = null;
        
        Destroy(obj.gameObject, lifetime);
    }

    private void OnApplicationQuit()
    {
        appQuitting = true;
    }
}
