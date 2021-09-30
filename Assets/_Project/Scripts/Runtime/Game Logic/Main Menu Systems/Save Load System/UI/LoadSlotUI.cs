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
    [SerializeField] private SceneLoadController sceneLoad = default;
    
    private string originText = "";

    public void Awake()
    {
        originText = $"Load Slot: {saveSlotFileName}";
        saveSlotText.text = originText;

        downTimer.CreateTimer(saveSlotEmptyTextDur);
        
        downTimer.OnTimerEnd += () => saveSlotText.text = originText;
        downTimer.OnTimerEnd += () => downTimer.gameObject.SetActive(false);

        downTimer.enabled = false;
    }

    public void OnClick_LoadIfSlotExists()
    {
        if (JSONSaveManager.SaveFileExists(saveSlotFileName))
        {
            Debug.Log("load save file logic cause save file exists yes");

            LoadSaveFile();
            
            return;
        }

        Debug.Log("save file is m.t.");
        saveSlotText.text = emptySaveSlotText;

        if (!downTimer.enabled) downTimer.enabled = true;
        downTimer.ResetTimer();
    }

    private void LoadSaveFile()
    {
        OnSceneLoadedCharacterDataLoad.LoadedCharacterData = (CharacterData) JSONSaveManager.LoadCharacterDataFile(saveSlotFileName);
        
        sceneLoad.LoadScene();
    }
}