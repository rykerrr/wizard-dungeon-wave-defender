using UnityEngine;
using WizardGame.ObjectRemovalHandling;

namespace WizardGame
{
    [RequireComponent(typeof(ITimedRemovalProcessor))]
    public class ParentObjToSceneOnDisable : MonoBehaviour
    {
        [SerializeField] private float lifetime = 10;

        private ITimedRemovalProcessor timedRemovalProcessor;

        private bool appQuitting = false;

        private void Awake()
        {
            timedRemovalProcessor = GetComponent<ITimedRemovalProcessor>();
        }

        private void OnDisable()
        {
            if (appQuitting) return;

            Debug.Log("Being disabled");

            transform.parent = null;

            Debug.Log(timedRemovalProcessor);
            timedRemovalProcessor.Remove(lifetime);
        }

        private void OnApplicationQuit()
        {
            appQuitting = true;
        }
    }
}