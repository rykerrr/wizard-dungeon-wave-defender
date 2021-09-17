using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WizardGame.MainMenu;
using WizardGame.Utility.Timers;

public class LoadSlotUI : MonoBehaviour
{
    [Header("Preferences")] [SerializeField]
    private string saveSlotFileName = "saveSlot1";

    [SerializeField] [TextArea] private string emptySaveSlotText = "Empty\nSave Slot";
    [SerializeField] private float saveSlotEmptyTextDur = 5f;

    [Header("References")] 
    [SerializeField] private TextMeshProUGUI saveSlotText = default;
    [SerializeField] private DownTimerBehaviour downTimer = default;

    private string originText = "";

    public void Awake()
    {
        originText = $"Load Slot: {saveSlotFileName}";
        saveSlotText.text = originText;

        downTimer.CreateTimer(saveSlotEmptyTextDur);
        
        downTimer.OnTimerEnd += () => saveSlotText.text = originText;
        downTimer.OnTimerEnd += () => downTimer.gameObject.SetActive(false);
        
        downTimer.gameObject.SetActive(false);
    }

    public void OnClick_LoadIfSlotExists()
    {
        if (JSONSaveManager.SaveFileExists(saveSlotFileName))
        {
            Debug.Log("load save file logic cause save file exists yes");

            return;
        }

        Debug.Log("save file is m.t.");
        saveSlotText.text = emptySaveSlotText;
        
        if(!downTimer.gameObject.activeSelf) downTimer.gameObject.SetActive(true);
        downTimer.ResetTimer();
    }
}