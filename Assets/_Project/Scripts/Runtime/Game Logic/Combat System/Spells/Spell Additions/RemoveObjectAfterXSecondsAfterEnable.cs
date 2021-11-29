using UnityEngine;
using WizardGame.ObjectRemovalHandling;
using WizardGame.Utility.Timers;

public class RemoveObjectAfterXSecondsAfterEnable : MonoBehaviour
{
    [SerializeField] private float time = 5f;
    
    private ITimedRemovalProcessor timedRemovalProcessor;
    
    private void Awake()
    {
        timedRemovalProcessor = GetComponent<ITimedRemovalProcessor>();
        timedRemovalProcessor.Remove(time);
    }
}
