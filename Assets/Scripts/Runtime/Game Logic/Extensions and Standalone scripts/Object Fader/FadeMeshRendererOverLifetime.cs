using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMeshRendererOverLifetime : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer = default;
    [SerializeField] private Color customColor = default;
    [SerializeField] private float duration = 1f;
    
    private Material material = default;

    private float lifeTime = 0f;

    public float Duration
    {
        get => duration;
        set => duration = value;
    }

    public Color CustomColor
    {
        get => customColor;
        set => customColor = value;
    }
    
    private void Awake()
    {
        meshRenderer ??= GetComponent<MeshRenderer>();
        
        material = meshRenderer.material;
    }

    private void Update()
    {
        Debug.Log("1st: " + Duration + " | " + (duration - lifeTime) + " | " + lifeTime);
        float t = (duration - lifeTime) / duration;
        Debug.Log("t: " + t);
        t = Mathf.Clamp01(t);
        Debug.Log("clamped t: " + t);
        float alpha = Mathf.Lerp(0f, 1f, t);
        Debug.Log("alpha: " + alpha);
        
        material.color = new Color(customColor.r, customColor.g, customColor.b, alpha);
        
        // lerp from 1 to 0
        // clamp01 duration - lifetime
        // lifetime += Time.deltaTime

        lifeTime = Mathf.Clamp(lifeTime + Time.deltaTime, 0, Duration);
    }
}
