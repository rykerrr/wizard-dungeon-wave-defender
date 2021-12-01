using System;
using System.Collections.Generic;
using UnityEngine;

public class ParentObjToSceneOnDisable : MonoBehaviour
{
    [SerializeField] private Transform obj;
    [SerializeField] private float lifetime = 10;
    [SerializeField] private float parentingDelay;
    
    private bool appQuitting = false;
    
    private void OnDisable()
    {
        if (appQuitting) return;
        
        Debug.Log("Being disabled");
        
        obj.parent = null;
        
        Destroy(obj.gameObject, lifetime);
    }

    private void OnDestroy()
    {
        Debug.Log("Being destroyed");
    }

    private void OnApplicationQuit()
    {
        appQuitting = true;
    }
}
