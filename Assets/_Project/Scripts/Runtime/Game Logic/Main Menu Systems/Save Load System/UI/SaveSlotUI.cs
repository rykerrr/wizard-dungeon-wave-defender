using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WizardGame.MainMenu;

public class SaveSlotUI : MonoBehaviour
{
    [Header("Preferences")] [SerializeField]
    private string saveSlotFileName = "saveSlot1";

    [Header("References")] [SerializeField]
    private GameObject savePopup = default;
    [SerializeField] private GameObject elementSelectionPanel = default;
    [SerializeField] private TextMeshProUGUI saveSlotText = default;

    private void Awake()
    {
        saveSlotText.text = $"Save Slot: {saveSlotFileName}";
    }

    public void OnClick_NewGameOrOpenPopup()
    {
        if (JSONSaveManager.SaveFileExists(saveSlotFileName))
        {
            savePopup.SetActive(true);
            Debug.Log("Save file already exists, overwrite?");

            return;
        }
        
        elementSelectionPanel.SetActive(true);
    }

    public void OnClick_ConfirmNewGameOverwrite()
    {
        elementSelectionPanel.SetActive(true);
    }
}