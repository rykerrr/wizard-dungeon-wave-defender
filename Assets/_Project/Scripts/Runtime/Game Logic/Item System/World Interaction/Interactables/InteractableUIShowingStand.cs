using UnityEngine;

namespace WizardGame.Item_System.World_Interaction
{
    public class InteractableUIShowingStand : MonoBehaviour, IInteractable
    {
        [Header("References")]
        [SerializeField] private GameObject uiObjectToShow = default;

        [Header("Preferences")] [SerializeField]
        private string overrideUiName = default;
        
        public GameObject UIObjectToShow => uiObjectToShow;
        public string InteractUseDescription => 
            $"show {(string.IsNullOrEmpty(overrideUiName) ? uiObjectToShow.name : overrideUiName)}";

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