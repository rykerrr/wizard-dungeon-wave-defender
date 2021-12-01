#if UNITY_EDITOR

using UnityEngine;

namespace WizardGame.ManualTestStuff
{
    public class InheritanceParent1 : MonoBehaviour
    {
        protected virtual string Gog => "Parent";

        private void Awake()
        {
            Debug.Log(Gog, this);
        }
    }
}

#endif