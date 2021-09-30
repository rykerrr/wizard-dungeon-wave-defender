#if UNITY_EDITOR

using UnityEngine;

namespace WizardGame.ManualTestStuff
{
    public class InheritanceParent1 : MonoBehaviour
    {
        protected void Awake()
        {
            Debug.Log("Parent");
        }
    }
}

#endif