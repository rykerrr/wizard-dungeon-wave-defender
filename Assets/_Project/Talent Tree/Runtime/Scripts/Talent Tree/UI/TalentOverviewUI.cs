using Talent_Tree.Dynamic_Talent_Tree;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Talent_Tree.UI
{
    public class TalentOverviewUI : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private DynamicTalentHandler dynamicTalentHandler = default;
        
        [Header("UI References")]
        [SerializeField] private Image talentIconImage = default;
        [SerializeField] private TextMeshProUGUI talentNameText = default;
        [SerializeField] private TextMeshProUGUI talentDescriptionText = default;
        [SerializeField] private TextMeshProUGUI talentLevelDisplayText = default;
        [SerializeField] private GameObject unlockTalentButton = default;
        [SerializeField] private GameObject talentIconParent = default;
        
        private DynamicTalentUI selectedTalentUi = default;

        private void Awake()
        {
            UpdateDisplay();
        }

        public void SelectTalent(DynamicTalentUI talentUi)
        {
            selectedTalentUi = talentUi;
            
            Debug.Log($"Selected talent {selectedTalentUi}");
            
            selectedTalentUi.onTalentLeveledUp += UpdateDisplay;

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (selectedTalentUi == null)
            {
                DisableOverview();

                return;
            }
            
            if(!unlockTalentButton.activeSelf) unlockTalentButton.SetActive(true);

            var talContainer = selectedTalentUi.TalentContainer;
            var talent = talContainer.Talent;

            talentNameText.text = talent.Name;
            talentDescriptionText.text = talent.Description;
            talentLevelDisplayText.text = 
                $"Current Level/Max Level: {talContainer.CurrentTalentLevel}/{talent.MaxTalentLevel}\n"
                + $"Points required for level up: {talent.SingleLevelWeight}";
            
            if(!talentIconParent.activeSelf) talentIconParent.SetActive(true);
            
            talentIconImage.sprite = talent.Icon;
        }

        private void DisableOverview()
        {
            if(unlockTalentButton.activeSelf) unlockTalentButton.SetActive(false);

            talentNameText.text = "";
            talentDescriptionText.text = "";
            talentLevelDisplayText.text = "";

            if(talentIconParent.activeSelf) talentIconParent.SetActive(false);
            talentIconImage.sprite = null;
        }

        private void DeselectTalentUI()
        {
            var selectedTalentNotNull = !ReferenceEquals(selectedTalentUi, null);
            if (selectedTalentNotNull)
            {
                selectedTalentUi.onTalentLeveledUp -= UpdateDisplay;
            }
            
            DisableOverview();
        }

        public void OnClick_TryUnlockTalent()
        {
            Debug.Log($"Attempting to level up talent: {selectedTalentUi}");
            
            dynamicTalentHandler.TryLevelupTalent(selectedTalentUi);
        }
    }
}
