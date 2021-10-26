using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WizardGame.Health_System;

public class HealthPercentTextDisplay : MonoBehaviour
{
    [SerializeField] private HealthSystemBehaviour healthSysBehav = default;
    [SerializeField] private TextMeshProUGUI healthPercentText = default;
    
    private void Awake()
    {
        healthSysBehav.HealthContainer.onHealthChange += ProcessHealthChange;
    }

    private void ProcessHealthChange(int curHealth, int maxHealth)
    {
        var hpFill = (float) curHealth / maxHealth;
        
        healthPercentText.text = $"{hpFill * 100:0.#}%";
    }
}
