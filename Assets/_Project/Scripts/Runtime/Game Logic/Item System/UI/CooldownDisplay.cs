using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WizardGame.Combat_System.Cooldown_System;

namespace WizardGame.Item_System.UI
{
    public class CooldownDisplay : MonoBehaviour
    {
        [Header("Display references")] [SerializeField]
        private Image cooldownFillImage = default;

        [SerializeField] private TextMeshProUGUI cooldownText = default;

        [Header("Dependencies")] [SerializeField]
        private CooldownSystem cdSystem = default;

        [Header("Properties")] [SerializeField]
        private Guid id;

        private RectTransform imgRectTransf = default;
        private Cooldown data = default;

        public Guid Id => id;

        private void Awake()
        {
            imgRectTransf = cooldownFillImage.rectTransform;

            Vector3 scale = imgRectTransf.localScale;
            imgRectTransf.localScale = new Vector3(scale.x, 0f, scale.z);
            
            cooldownText.text = string.Empty;
        }
        
        public void UpdateData(IHasCooldown cd)
        {
            if (ReferenceEquals(cd, null))
            {
                ClearCooldownData();

                return;
            }
            
            var newData = cdSystem.GetCooldown(cd.Id);

            var curDataNotNull = !ReferenceEquals(data, null);
            var sameData = curDataNotNull && ReferenceEquals(data, newData);
            if (sameData) return;
            
            data?.RemoveListenAction(UpdateDisplay);

            id = cd.Id;
            data = newData;

            data?.AddListenAction(UpdateDisplay);
        }

        public void ClearCooldownData()
        {
            data?.RemoveListenAction(UpdateDisplay);

            data = null;
            id = Guid.Empty;

            UpdateDisplay(0, 0);
        }

        public void UpdateDisplay(float remainingTime, float actualTime)
        {
            var fill = Mathf.Clamp01((float) Math.Round(remainingTime / actualTime, 2));
            if (Single.IsNaN(fill)) fill = Mathf.Epsilon;

            var scale = imgRectTransf.localScale;
            imgRectTransf.localScale = new Vector3(scale.x, fill, scale.z);

            cooldownText.text = remainingTime == 0 ? string.Empty : $"{remainingTime}s";
        }

        #region debug
        #if UNITY_EDITOR
        [ContextMenu("Log debug data")]
        public void LogDebugData()
        {
            Debug.Log($"{data} | {Id} | {cdSystem}");
        }

        [ContextMenu("Add as listener")]
        public void TryAddAsListener()
        {
            var cd = cdSystem.GetCooldown(id);
            
            Debug.Log("Cooldown: " + cd);
            
            cd?.AddListenAction(UpdateDisplay);
        }

        [ContextMenu("Remove as listener")]
        public void TryRemoveAsListener()
        {
            var cd = cdSystem.GetCooldown(id);
            
            Debug.Log("Cooldown: " + cd);
            
            cd?.RemoveListenAction(UpdateDisplay);
        }
        #endif
        #endregion
    }
}