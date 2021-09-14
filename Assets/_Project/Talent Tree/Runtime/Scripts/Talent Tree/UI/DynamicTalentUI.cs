using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Talent_Tree.UI
{
    public class DynamicTalentUI : MonoBehaviour
    {
        [Header("UI References")] [SerializeField]
        private Image iconImage = default;

        [SerializeField] private Image tintImage = default;
        [SerializeField] private TextMeshProUGUI countText = default;

        [Header("Talent properties")] [SerializeField]
        private TalentContainer talentContainer = default;

        [SerializeField] private UnlockState unlockState = UnlockState.NotUnlockable;
        [SerializeField] private Color unlockedTintColor = Color.white;

        [SerializeField] private List<DynamicTalentLinkUI> links = new List<DynamicTalentLinkUI>();

        public event Action onTalentUnlocked = delegate { };
        public event Action onTalentLeveledUp = delegate { };

        public TalentContainer TalentContainer => talentContainer;
        public List<DynamicTalentLinkUI> Links => links;

        public void Init(Talent talent, List<DynamicTalentLinkUI> talentLinks
            , UnlockState unlockStateOnDefault = UnlockState.NotUnlockable)
        {
            countText.gameObject.SetActive(false);

            talentContainer = new TalentContainer(talent);

            unlockState = unlockStateOnDefault;
            links = talentLinks;

            UpdateImageIcon();
            UpdateTalentUI();
        }

        private void UpdateImageIcon()
        {
            iconImage.sprite = talentContainer.Talent.Icon;
        }

        public bool TryLevelUp(int points)
        {
            if (unlockState != UnlockState.Unlockable && unlockState != UnlockState.Unlocked)
            {
                Debug.LogWarning("Can't unlock at the moment, unlock state: " + unlockState);

                return false;
            }

            var levelUpResult = talentContainer.TryLevelupTalent(points);
            if (!levelUpResult) return false;

            if (talentContainer.CurrentTalentLevel >= 1)
            {
                if (talentContainer.CurrentTalentLevel == 1)
                {
                    onTalentUnlocked?.Invoke();

                    unlockState = UnlockState.Unlocked;
                }

                if (links != null && links.Count > 0)
                {
                    foreach (var link in links)
                    {
                        if (!link.CanTraverse(talentContainer.CurrentTalentLevel)) continue;


                        link.Destination.TrySetUnlockState(UnlockState.Unlockable);
                    }

                    UpdateLinksUI();
                }
            }

            UpdateTalentUI();

            onTalentLeveledUp?.Invoke();

            return true;
        }

        private void TrySetUnlockState(UnlockState newState)
        {
            switch (newState)
            {
                case UnlockState.NotUnlockable:
                {
                    unlockState = newState;

                    break;
                }
                case UnlockState.Unlockable:
                {
                    if (unlockState == UnlockState.Unlocked) return;

                    unlockState = newState;

                    break;
                }
                case UnlockState.Unlocked:
                {
                    if (unlockState == UnlockState.NotUnlockable) return;

                    unlockState = newState;

                    break;
                }
            }

            UpdateTalentUI();
        }

        private void UpdateTalentUI()
        {
            if (unlockState == UnlockState.Unlockable || unlockState == UnlockState.Unlocked)
            {
                if (!countText.gameObject.activeSelf)
                {
                    countText.gameObject.SetActive(true);
                }

                tintImage.color = unlockedTintColor;
            }

            countText.text = $"{TalentContainer.CurrentTalentLevel}/{TalentContainer.Talent.MaxTalentLevel}";
        }

        private void UpdateLinksUI()
        {
            foreach (var link in links)
            {
                link.UpdateFill(talentContainer.CurrentTalentLevel);
            }
        }
    }
}