﻿using UnityEngine;

namespace WizardGame.Item_System.World_Interaction
{
    public class InteractableUIShowingStand : MonoBehaviour, IInteractable
    {
        [Header("References")]
        [SerializeField] private GameObject uiObjectToShow = default;

        [Header("Preferences")] [SerializeField]
        private string interactableDescription = default;
        
        public GameObject UIObjectToShow => uiObjectToShow;
        public string InteractableDescription => interactableDescription;

        public void OnCharacterEnter(Transform plr)
        {
            throw new System.NotImplementedException();
        }

        public void OnCharacterExit(Transform plr)
        {
            throw new System.NotImplementedException();
        }
    }
}