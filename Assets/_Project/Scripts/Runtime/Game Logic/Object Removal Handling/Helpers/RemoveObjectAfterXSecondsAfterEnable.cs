using UnityEngine;
using WizardGame.ObjectRemovalHandling;

namespace WizardGame.ObjectRemovalHandling
{
    public class RemoveObjectAfterXSecondsAfterEnable : MonoBehaviour
    {
        [SerializeField] private float time = 5f;

        private ITimedRemovalProcessor timedRemovalProcessor;

        /// <summary>
        /// I guess you could call this a hack, but it's due to how Unity's method invocation works
        /// Unity guarantees that an object's OnEnable will be called AFTER its Awake method
        /// But it doesn't guarantee that OnEnable methods will get invoked after all Awake calls
        /// have been invoked on every object
        /// But Start methods will get invoked only after all Awake methods were 
        /// </summary>
        private bool calledInStart = false;

        private void Awake()
        {
            timedRemovalProcessor = GetComponent<ITimedRemovalProcessor>();
        }

        private void Start()
        {
            timedRemovalProcessor.Remove(time);
            calledInStart = true;
        }

        private void OnEnable()
        {
            if (!calledInStart) return;

            timedRemovalProcessor.Remove(time);
        }
    }
}
