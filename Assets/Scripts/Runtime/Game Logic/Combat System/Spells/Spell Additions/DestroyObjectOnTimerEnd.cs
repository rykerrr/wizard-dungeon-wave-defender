using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WizardGame.Utility.Timers;

public class DestroyObjectOnTimerEnd : MonoBehaviour
{
    [SerializeField] private GameObject objToDestroy = default;
    [SerializeField] private float time = 5f;

    private ITimer timer;
    
    private void Awake()
    {
        DownTimer newTimer = new DownTimer(time);
        
        newTimer.OnTimerEnd += () => Destroy(objToDestroy);
        timer = newTimer;
    }

    private void Update()
    {
        timer.TryTick(Time.deltaTime);
    }
}
