using TMPro;
using UnityEngine;

namespace WizardGame.Item_System.World_Interaction.UI
{
    public class InteractButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI interactText = default;

        public void UpdateUI(string useText)
        {
            TryEnable();
            var button = "[Placeholder Button, E]";
            
            interactText.text = $"Press {button} to {useText}";
        }

        public void TryEnable()
        {
            if(!gameObject.activeSelf) gameObject.SetActive(true);
        }
        
        public void TryDisable()
        {
            if(gameObject.activeSelf) gameObject.SetActive(false);
        }
        
        public void OnClick_DoInteract(PlayerInteractionBehavior interactor)
        {
            interactor.TryProcessInteraction();
        }
    }
}