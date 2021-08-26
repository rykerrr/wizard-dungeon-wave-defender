using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WizardGame.Health_System;

public class HealthDisplayUI : MonoBehaviour
{
    [SerializeField] private HealthSystemBehaviour healthSysBehav = default;
    [SerializeField] private Image healthFillImg = default;
    [SerializeField] private TextMeshProUGUI healthPercentText = default;

    [SerializeField] private Gradient hpColGradient = default;
    
    private RectTransform healthFillImgRect = default;
    
    private void Awake()
    {
        healthSysBehav.HealthSystem.onHealthChange += ProcessHealthChange;
        healthFillImgRect = healthFillImg.rectTransform;
    }

    private void ProcessHealthChange(int curHealth, int maxHealth)
    {
        var healthFillScale = healthFillImgRect.localScale;
        var hpFill = (float) curHealth / maxHealth;
        
        healthFillImgRect.localScale = new Vector3(hpFill, healthFillScale.y,
            healthFillScale.z);

        var newCol = ColorFromGradient(hpFill);
        healthFillImg.color = newCol;

        healthPercentText.text = $"{hpFill * 100:0.#}%";
    }

    private Color ColorFromGradient(float val)
    {
        return hpColGradient.Evaluate(val);
    }
}
