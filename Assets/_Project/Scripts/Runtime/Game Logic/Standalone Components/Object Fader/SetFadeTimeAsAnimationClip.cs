using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFadeTimeAsAnimationClip : MonoBehaviour
{
    [SerializeField] private FadeMeshRendererOverLifetime fader;
    [SerializeField] private AnimationClip clip;

    private void Awake()
    {
        fader.Duration = clip.length;
    }
}
