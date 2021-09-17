using UnityEngine;

namespace WizardGame.SelectionWindow
{
    public class SelectElementFromOverviewOnClick : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RotatingWindowElementOverview overviewWindow = default;
        [SerializeField] private ElementSelectionConfirmPopup popup = default;
        
        public void OnClick_ProcessSelect()
        {
            popup.gameObject.SetActive(true);
            
            // popup.UpdateUI(overviewWindow.SelectedElement.Data);
        }
    }
}