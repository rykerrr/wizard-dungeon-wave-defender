#if UNITY_EDITOR

using UnityEngine;

namespace WizardGame.ManualTestStuff
{
    public class InheritanceChild1 : InheritanceParent1
    {
        private new void Awake()
        {  
            Debug.Log("Child"); 
        }
    }
}

#endif